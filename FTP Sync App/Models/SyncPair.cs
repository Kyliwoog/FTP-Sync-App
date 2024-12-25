namespace FtpSyncApp.Models
{
    public class SyncPair
    {
        public string LocalPath { get; set; } // Chemin local
        public string RemotePath { get; set; } // Chemin distant (FTP)
        public string ServerName { get; set; } // Nom du serveur FTP

        // Propriété pour afficher les informations dans l'application
        public string Display => $"{LocalPath} -> {RemotePath}";
    }
}
