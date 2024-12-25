using FtpSyncApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace FtpSyncApp.Services
{
    public static class LoggerService
    {
        private static string _logFilePath = "transfer_logs.json";

        public static void LogTransfer(TransferLog log)
        {
            var logs = LoadLogs();
            logs.Add(log);
            SaveLogs(logs);
        }

        private static readonly string LogFilePath = "logs.txt";

        public static void Log(string message)
        {
            try
            {
                string logMessage = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}";
                File.AppendAllText(LogFilePath, logMessage + Environment.NewLine);
            }
            catch (Exception ex)
            {
                // Gérer les erreurs de journalisation ici si nécessaire
                Console.WriteLine($"Erreur lors de l'écriture des logs : {ex.Message}");
            }
        }
        public static List<TransferLog> LoadLogs()
        {
            if (!File.Exists(_logFilePath))
                return new List<TransferLog>();

            try
            {
                string json = File.ReadAllText(_logFilePath);
                return JsonSerializer.Deserialize<List<TransferLog>>(json)
                       ?? new List<TransferLog>();
            }
            catch
            {
                return new List<TransferLog>();
            }
        }

        private static void SaveLogs(List<TransferLog> logs)
        {
            try
            {
                string json = JsonSerializer.Serialize(logs, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(_logFilePath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur sauvegarde logs : {ex.Message}");
            }
        }
    }
}
