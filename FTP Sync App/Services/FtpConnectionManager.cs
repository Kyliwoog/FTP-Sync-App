using System;
using System.Collections.Generic;
using System.IO;
using FtpSyncApp.Models;
using Newtonsoft.Json;
using NewtonsoftJson = Newtonsoft.Json;

namespace FtpSyncApp.Services
{
    public static class FtpConnectionManager
    {
        private static string _filePath = "ftp_connections.json";

        /// <summary>
        /// Charge les connexions FTP à partir du fichier JSON.
        /// </summary>
        /// <returns>Liste des connexions FTP.</returns>
        public static List<FtpConnection> LoadConnections()
        {
            if (!File.Exists(_filePath))
                return new List<FtpConnection>();

            try
            {
                string json = File.ReadAllText(_filePath);
                return NewtonsoftJson.JsonConvert.DeserializeObject<List<FtpConnection>>(json) ?? new List<FtpConnection>();
            }
            catch
            {
                return new List<FtpConnection>();
            }
        }

        /// <summary>
        /// Sauvegarde les connexions FTP dans un fichier JSON.
        /// </summary>
        /// <param name="connections">Liste des connexions FTP.</param>
        public static void SaveConnections(List<FtpConnection> connections)
        {
            try
            {
                string json = NewtonsoftJson.JsonConvert.SerializeObject(connections, NewtonsoftJson.Formatting.Indented);
                File.WriteAllText(_filePath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur sauvegarde connexions : {ex.Message}");
            }
        }

        /// <summary>
        /// Sauvegarde les paires de synchronisation pour un serveur donné.
        /// </summary>
        /// <param name="serverName">Nom du serveur.</param>
        /// <param name="pairs">Liste des paires de synchronisation.</param>
        public static void SaveSyncPairs(string serverName, List<SyncPair> pairs)
        {
            var filePath = $"{serverName}_sync_pairs.json";
            try
            {
                var json = NewtonsoftJson.JsonConvert.SerializeObject(pairs, NewtonsoftJson.Formatting.Indented);
                File.WriteAllText(filePath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur sauvegarde des paires : {ex.Message}");
            }
        }

        /// <summary>
        /// Charge les paires de synchronisation pour un serveur donné.
        /// </summary>
        /// <param name="serverName">Nom du serveur.</param>
        /// <returns>Liste des paires de synchronisation.</returns>
        public static List<SyncPair> LoadSyncPairs(string serverName)
        {
            var filePath = $"{serverName}_sync_pairs.json";
            if (File.Exists(filePath))
            {
                try
                {
                    var json = File.ReadAllText(filePath);
                    return NewtonsoftJson.JsonConvert.DeserializeObject<List<SyncPair>>(json) ?? new List<SyncPair>();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erreur chargement des paires : {ex.Message}");
                }
            }

            return new List<SyncPair>();
        }
    }
}
