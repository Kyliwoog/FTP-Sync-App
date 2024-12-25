namespace FtpSyncApp.Forms
{
    partial class FormMain
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.ComboBox cmbConnections;
        private System.Windows.Forms.Button btnAddConnection;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Button btnDisconnect;
        private System.Windows.Forms.Button btnSelectLocalFolder;
        private System.Windows.Forms.Button btnAddPair;
        private System.Windows.Forms.TextBox txtLocalFolder;
        private System.Windows.Forms.TreeView treeRemoteDirectories;
        private System.Windows.Forms.Panel pnlConnectionStatus;
        private System.Windows.Forms.ListBox lstPairs;
        private System.Windows.Forms.ListView lvLogs;
        private System.Windows.Forms.ColumnHeader columnAction;
        private System.Windows.Forms.ColumnHeader columnLocalPath;
        private System.Windows.Forms.ColumnHeader columnRemotePath;
        private System.Windows.Forms.ColumnHeader columnStatus;
        private ToolTip lvToolTip;

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            cmbConnections = new ComboBox();
            btnAddConnection = new Button();
            btnConnect = new Button();
            btnDisconnect = new Button();
            btnSelectLocalFolder = new Button();
            btnAddPair = new Button();
            txtLocalFolder = new TextBox();
            treeRemoteDirectories = new TreeView();
            pnlConnectionStatus = new Panel();
            lstPairs = new ListBox();
            lvLogs = new ListView();
            columnAction = new ColumnHeader();
            columnLocalPath = new ColumnHeader();
            columnRemotePath = new ColumnHeader();
            columnStatus = new ColumnHeader();
            lvToolTip = new ToolTip(components);
            btnRemovePair = new Button();
            SuspendLayout();
            // 
            // cmbConnections
            // 
            cmbConnections.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbConnections.Location = new Point(12, 12);
            cmbConnections.Name = "cmbConnections";
            cmbConnections.Size = new Size(661, 23);
            cmbConnections.TabIndex = 1;
            // 
            // btnAddConnection
            // 
            btnAddConnection.Location = new Point(679, 12);
            btnAddConnection.Name = "btnAddConnection";
            btnAddConnection.Size = new Size(68, 23);
            btnAddConnection.TabIndex = 2;
            btnAddConnection.Text = "Ajouter";
            btnAddConnection.Click += btnAddConnection_Click;
            // 
            // btnConnect
            // 
            btnConnect.Location = new Point(753, 12);
            btnConnect.Name = "btnConnect";
            btnConnect.Size = new Size(87, 23);
            btnConnect.TabIndex = 3;
            btnConnect.Text = "Connecter";
            btnConnect.Click += btnConnect_Click;
            // 
            // btnDisconnect
            // 
            btnDisconnect.Location = new Point(846, 12);
            btnDisconnect.Name = "btnDisconnect";
            btnDisconnect.Size = new Size(95, 23);
            btnDisconnect.TabIndex = 4;
            btnDisconnect.Text = "Déconnecter";
            btnDisconnect.Click += btnDisconnect_Click;
            // 
            // btnSelectLocalFolder
            // 
            btnSelectLocalFolder.Location = new Point(12, 50);
            btnSelectLocalFolder.Name = "btnSelectLocalFolder";
            btnSelectLocalFolder.Size = new Size(120, 23);
            btnSelectLocalFolder.TabIndex = 5;
            btnSelectLocalFolder.Text = "Parcourir Local";
            btnSelectLocalFolder.Click += btnSelectLocalFolder_Click;
            // 
            // btnAddPair
            // 
            btnAddPair.Location = new Point(679, 50);
            btnAddPair.Name = "btnAddPair";
            btnAddPair.Size = new Size(87, 23);
            btnAddPair.TabIndex = 7;
            btnAddPair.Text = "Ajouter Paire";
            btnAddPair.Click += btnAddPair_Click;
            // 
            // txtLocalFolder
            // 
            txtLocalFolder.Location = new Point(138, 50);
            txtLocalFolder.Name = "txtLocalFolder";
            txtLocalFolder.Size = new Size(535, 23);
            txtLocalFolder.TabIndex = 6;
            // 
            // treeRemoteDirectories
            // 
            treeRemoteDirectories.Location = new Point(12, 234);
            treeRemoteDirectories.Name = "treeRemoteDirectories";
            treeRemoteDirectories.Size = new Size(457, 300);
            treeRemoteDirectories.TabIndex = 9;
            treeRemoteDirectories.AfterSelect += treeRemoteDirectories_AfterSelect;
            treeRemoteDirectories.NodeMouseDoubleClick += treeRemoteDirectories_NodeMouseDoubleClick;
            // 
            // pnlConnectionStatus
            // 
            pnlConnectionStatus.BackColor = Color.Red;
            pnlConnectionStatus.Location = new Point(954, 15);
            pnlConnectionStatus.Name = "pnlConnectionStatus";
            pnlConnectionStatus.Size = new Size(18, 20);
            pnlConnectionStatus.TabIndex = 10;
            // 
            // lstPairs
            // 
            lstPairs.ItemHeight = 15;
            lstPairs.Location = new Point(12, 80);
            lstPairs.Name = "lstPairs";
            lstPairs.Size = new Size(457, 139);
            lstPairs.TabIndex = 8;
            // 
            // lvLogs
            // 
            lvLogs.Columns.AddRange(new ColumnHeader[] { columnAction, columnLocalPath, columnRemotePath, columnStatus });
            lvLogs.FullRowSelect = true;
            lvLogs.GridLines = true;
            lvLogs.Location = new Point(475, 79);
            lvLogs.Name = "lvLogs";
            lvLogs.Size = new Size(497, 454);
            lvLogs.TabIndex = 0;
            lvLogs.UseCompatibleStateImageBehavior = false;
            lvLogs.View = View.Details;
            lvLogs.MouseMove += lvLogs_MouseMove;
            // 
            // columnAction
            // 
            columnAction.Width = 100;
            // 
            // columnLocalPath
            // 
            columnLocalPath.Width = 190;
            // 
            // columnRemotePath
            // 
            columnRemotePath.Width = 100;
            // 
            // btnRemovePair
            // 
            btnRemovePair.Location = new Point(772, 49);
            btnRemovePair.Name = "btnRemovePair";
            btnRemovePair.Size = new Size(119, 23);
            btnRemovePair.TabIndex = 11;
            btnRemovePair.Text = "Supprimer Paire";
            btnRemovePair.UseVisualStyleBackColor = true;
            btnRemovePair.Click += btnRemovePair_Click;
            // 
            // FormMain
            // 
            ClientSize = new Size(979, 540);
            Controls.Add(btnRemovePair);
            Controls.Add(lvLogs);
            Controls.Add(cmbConnections);
            Controls.Add(btnAddConnection);
            Controls.Add(btnConnect);
            Controls.Add(btnDisconnect);
            Controls.Add(btnSelectLocalFolder);
            Controls.Add(txtLocalFolder);
            Controls.Add(btnAddPair);
            Controls.Add(lstPairs);
            Controls.Add(treeRemoteDirectories);
            Controls.Add(pnlConnectionStatus);
            Name = "FormMain";
            Text = "FTP Sync App";
            Load += FormMain_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        private Button btnRemovePair;
    }
}
