using System;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using FtpSyncApp.Models;
using FtpSyncApp.Services;

namespace FtpSyncApp.Forms
{
    public partial class FormConnection : Form
    {
        private FtpConnection? _currentConnection;

        private bool _isEditMode;

        public FormConnection(FtpConnection? conn = null)
        {
            InitializeComponent(); // Pense à générer l’interface dans le designer
            _currentConnection = conn;
            _isEditMode = (conn != null);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void FormConnection_Load(object sender, EventArgs e)
        {
            if (_isEditMode && _currentConnection != null)
            {
                txtName.Text = _currentConnection.Name;
                txtServerUrl.Text = _currentConnection.ServerUrl;
                txtPort.Text = _currentConnection.Port.ToString();
                txtUsername.Text = _currentConnection.Username;
                txtPassword.Text = _currentConnection.Password;
                chkUseFtps.Checked = _currentConnection.UseFtps;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!_isEditMode)
            {
                _currentConnection = new FtpConnection();
            }

            _currentConnection.Name = txtName.Text;
            _currentConnection.ServerUrl = txtServerUrl.Text;
            _currentConnection.Port = int.TryParse(txtPort.Text, out int p) ? p : 21;
            _currentConnection.Username = txtUsername.Text;
            _currentConnection.Password = txtPassword.Text;
            _currentConnection.UseFtps = chkUseFtps.Checked;

            var allConns = FtpConnectionManager.LoadConnections();

            if (!_isEditMode)
            {
                // Ajout
                allConns.Add(_currentConnection);
            }
            else
            {
                // Mise à jour
                var existing = allConns.FirstOrDefault(c => c.Name == _currentConnection.Name);
                if (existing != null)
                {
                    // Si tu gères une clé unique par "Name", c’est ici que tu modifies
                }
            }

            // Sauvegarde
            FtpConnectionManager.SaveConnections(allConns);

            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
