namespace FtpSyncApp.Models
{
    public class FtpConnection
    {
        public string Name { get; set; }
        public string ServerUrl { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int Port { get; set; } = 21;
        public bool UseFtps { get; set; }
        public List<SyncPair> SyncPairs { get; set; } = new List<SyncPair>();
    }
}
