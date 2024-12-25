using System;
using System.Windows.Forms;
using FtpSyncApp.Forms; // Assure-toi d'inclure le namespace de FormMain

namespace FtpSyncApp
{
    static class Program
    {
        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Ouvre FormMain
            Application.Run(new FormMain());
        }
    }
}
