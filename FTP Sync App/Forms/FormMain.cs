using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using FtpSyncApp.Models;
using FtpSyncApp.Services;

namespace FtpSyncApp.Forms
{
    public partial class FormMain : Form
    {
        private List<FtpConnection> _connections = new List<FtpConnection>();
        private FtpSyncService _ftpSyncService = new FtpSyncService();
        private string _selectedRemoteDirectory;
        private List<SyncPair> _syncPairs = new List<SyncPair>();
        private FileSystemWatcher _watcher;

        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            LoadConnections();
            RefreshConnectionsList();
            if (cmbConnections.SelectedItem is FtpConnection selectedConnection)
            {
                _syncPairs = FtpConnectionManager.LoadSyncPairs(selectedConnection.Name);
                RefreshSyncPairs();
            }
        }

        private void LoadConnections()
        {
            _connections = FtpConnectionManager.LoadConnections();
        }

        private void RefreshConnectionsList()
        {
            cmbConnections.DataSource = null;
            cmbConnections.DataSource = _connections;
            cmbConnections.DisplayMember = "Name";
        }

        private void btnAddConnection_Click(object sender, EventArgs e)
        {
            var formConn = new FormConnection();
            if (formConn.ShowDialog() == DialogResult.OK)
            {
                LoadConnections();
                RefreshConnectionsList();
            }
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                var selectedConnection = cmbConnections.SelectedItem as FtpConnection;
                if (selectedConnection != null)
                {
                    _ftpSyncService.Connect(selectedConnection);
                    pnlConnectionStatus.BackColor = Color.Green;

                    // Charger les paires
                    _syncPairs = FtpConnectionManager.LoadSyncPairs(selectedConnection.Name);
                    RefreshSyncPairs();

                    // Charger les dossiers distants
                    var rootDirectories = _ftpSyncService.GetRemoteDirectories("/");
                    treeRemoteDirectories.Nodes.Clear();
                    foreach (var dir in rootDirectories)
                    {
                        treeRemoteDirectories.Nodes.Add(new TreeNode(dir));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur : {ex.Message}");
                pnlConnectionStatus.BackColor = Color.Red;
            }
        }


        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            try
            {
                var selectedConnection = cmbConnections.SelectedItem as FtpConnection;
                if (selectedConnection != null)
                {
                    // Sauvegarder les paires
                    FtpConnectionManager.SaveSyncPairs(selectedConnection.Name, _syncPairs);
                }

                _ftpSyncService.Disconnect();
                pnlConnectionStatus.BackColor = Color.Red;
                MessageBox.Show("Déconnecté du serveur FTP.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de la déconnexion : {ex.Message}");
            }
        }



        private void btnSelectLocalFolder_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    txtLocalFolder.Text = fbd.SelectedPath;
                }
            }
        }

        private void btnAddPair_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtLocalFolder.Text) || string.IsNullOrEmpty(_selectedRemoteDirectory))
            {
                MessageBox.Show("Veuillez sélectionner un dossier local et un dossier distant.");
                return;
            }

            var pair = new SyncPair
            {
                LocalPath = txtLocalFolder.Text,
                RemotePath = _selectedRemoteDirectory
            };

            _syncPairs.Add(pair);

            SaveSyncPairs();
            RefreshSyncPairs();

            // Configurer le FileSystemWatcher pour la paire ajoutée
            StartWatching(pair.LocalPath);
            MessageBox.Show("Paire ajoutée avec succès.");
        }

        private void SaveSyncPairs()
        {
            var selectedConnection = cmbConnections.SelectedItem as FtpConnection;
            if (selectedConnection != null)
            {
                FtpConnectionManager.SaveSyncPairs(selectedConnection.Name, _syncPairs);
            }
        }


        private void RefreshSyncPairs()
        {
            lstPairs.DataSource = null;
            lstPairs.DataSource = _syncPairs;
            lstPairs.DisplayMember = nameof(SyncPair.Display);
        }


        private void StartWatching(string path)
        {
            if (_watcher != null)
            {
                _watcher.Dispose();
            }

            _watcher = new FileSystemWatcher(path)
            {
                NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName,
                IncludeSubdirectories = true,
                EnableRaisingEvents = true
            };

            _watcher.Changed += OnFileChanged;
            _watcher.Created += OnFileChanged;
            _watcher.Deleted += OnFileChanged;
            _watcher.Renamed += OnFileRenamed;
        }

        private void OnFileChanged(object sender, FileSystemEventArgs e)
        {
            switch (e.ChangeType)
            {
                case WatcherChangeTypes.Created:
                case WatcherChangeTypes.Changed:
                    SyncFolderAutomatically(e.FullPath);
                    break;

                case WatcherChangeTypes.Deleted:
                    DeleteFileFromFtp(e.FullPath);
                    break;
            }
        }

        private void DeleteFileFromFtp(string localFilePath)
        {
            try
            {
                var pair = _syncPairs.Find(p => localFilePath.StartsWith(p.LocalPath));

                if (pair != null)
                {
                    var relativePath = Path.GetRelativePath(pair.LocalPath, localFilePath);
                    var remoteFilePath = Path.Combine(pair.RemotePath, relativePath).Replace("\\", "/");

                    _ftpSyncService.DeleteFile(remoteFilePath);
                    LoggerService.Log($"Fichier supprimé : {remoteFilePath}");

                    AddLogToListView(
                        "Suppression",
                        localFilePath,
                        remoteFilePath,
                        "Succès"
                    );
                }
            }
            catch (Exception ex)
            {
                AddLogToListView(
                    "Suppression",
                    localFilePath,
                    "N/A",
                    $"Erreur : {ex.Message}"
                );
            }
        }


        private void AddLogToListView(string action, string localPath, string remotePath, string status)
        {
            if (lvLogs.InvokeRequired)
            {
                lvLogs.Invoke(new Action(() => AddLogToListView(action, localPath, remotePath, status)));
            }
            else
            {
                var listItem = new ListViewItem(action);
                listItem.SubItems.Add(localPath);
                listItem.SubItems.Add(remotePath);
                listItem.SubItems.Add(status);
                lvLogs.Items.Add(listItem);

                // Scroller automatiquement vers le dernier élément
                lvLogs.EnsureVisible(lvLogs.Items.Count - 1);
            }
        }

        private void lvLogs_MouseMove(object sender, MouseEventArgs e)
        {
            var hitTestInfo = lvLogs.HitTest(e.Location);
            if (hitTestInfo.Item != null)
            {
                var text = string.Join(" | ", hitTestInfo.Item.SubItems[0].Text, hitTestInfo.Item.SubItems[1].Text, hitTestInfo.Item.SubItems[2].Text, hitTestInfo.Item.SubItems[3].Text);
                lvToolTip.SetToolTip(lvLogs, text);
            }
            else
            {
                lvToolTip.SetToolTip(lvLogs, string.Empty);
            }
        }


        private void OnFileRenamed(object sender, RenamedEventArgs e)
        {
            SyncFolderAutomatically(e.FullPath);
        }

        private void SyncFolderAutomatically(string path)
        {
            try
            {
                var pair = _syncPairs.Find(p => path.StartsWith(p.LocalPath));

                if (pair != null)
                {
                    _ftpSyncService.SyncFolder(pair.LocalPath, pair.RemotePath);
                    LoggerService.Log($"Synchronisation automatique effectuée pour {pair.LocalPath} -> {pair.RemotePath}");

                    AddLogToListView(
                        "Synchronisation",
                        pair.LocalPath,
                        pair.RemotePath,
                        "Succès"
                    );
                }
            }
            catch (Exception ex)
            {
                AddLogToListView(
                    "Synchronisation",
                    path,
                    "N/A",
                    $"Erreur : {ex.Message}"
                );
            }
        }



        private void SyncChanges(string path)
        {
            var pair = _syncPairs.Find(p => p.LocalPath == path);
            if (pair != null)
            {
                _ftpSyncService.SyncFolder(pair.LocalPath, pair.RemotePath);
            }
        }

        private void treeRemoteDirectories_AfterSelect(object sender, TreeViewEventArgs e)
        {
            _selectedRemoteDirectory = e.Node.FullPath;
        }


        private void treeRemoteDirectories_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            var selectedDirectory = e.Node.FullPath;

            try
            {
                var subDirectories = _ftpSyncService.GetRemoteDirectories(selectedDirectory);
                e.Node.Nodes.Clear();
                foreach (var dir in subDirectories)
                {
                    e.Node.Nodes.Add(new TreeNode(dir));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors du chargement : {ex.Message}");
            }
        }

        private void btnRemovePair_Click(object sender, EventArgs e)
        {
            if (lstPairs.SelectedItem == null)
            {
                MessageBox.Show("Veuillez sélectionner une paire à supprimer.");
                return;
            }

            var selectedPair = (SyncPair)lstPairs.SelectedItem;
            _syncPairs.Remove(selectedPair);

            SaveSyncPairs();
            RefreshSyncPairs();
            MessageBox.Show("Paire supprimée avec succès.");
        }
    }
}
