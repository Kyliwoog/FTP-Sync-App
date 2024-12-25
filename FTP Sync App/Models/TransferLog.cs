using System;

namespace FtpSyncApp.Models
{
    public class TransferLog
    {
        public DateTime Timestamp { get; set; }
        public string FilePath { get; set; }
        public string Action { get; set; }      // Upload, Download, Delete
        public bool Success { get; set; }
        public string Message { get; set; }     // Info ou message d’erreur
    }
}
