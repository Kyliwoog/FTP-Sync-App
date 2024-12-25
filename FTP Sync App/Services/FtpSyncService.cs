using FluentFTP;
using FtpSyncApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace FtpSyncApp.Services
{
    public class FtpSyncService
    {
        private FtpClient _ftpClient;

        public void Connect(FtpConnection conn)
        {
            _ftpClient = new FtpClient(conn.ServerUrl, new NetworkCredential(conn.Username, conn.Password))
            {
                Port = conn.Port
            };

            if (conn.UseFtps)
            {
                _ftpClient.Config.EncryptionMode = FtpEncryptionMode.Explicit;
                _ftpClient.ValidateCertificate += (control, e) => e.Accept = true;
            }

            _ftpClient.Connect();
        }


        public void DeleteFile(string remoteFilePath)
        {
            if (_ftpClient == null || !_ftpClient.IsConnected)
            {
                throw new Exception("FTP non connecté.");
            }

            if (_ftpClient.FileExists(remoteFilePath))
            {
                try
                {
                    _ftpClient.DeleteFile(remoteFilePath);
                    LoggerService.Log($"Fichier supprimé : {remoteFilePath}");
                }
                catch (Exception ex)
                {
                    LoggerService.Log($"Erreur lors de la suppression : {remoteFilePath}, Erreur : {ex.Message}");
                    throw;
                }
            }
        }



        public void SyncFolder(string localFolder, string remoteFolder)
        {
            if (_ftpClient == null || !_ftpClient.IsConnected)
            {
                throw new Exception("FTP non connecté.");
            }

            var localFiles = Directory.GetFiles(localFolder, "*", SearchOption.AllDirectories);

            foreach (var file in localFiles)
            {
                var relativePath = Path.GetRelativePath(localFolder, file);
                var remoteFilePath = Path.Combine(remoteFolder, relativePath).Replace("\\", "/");

                try
                {
                    _ftpClient.UploadFile(file, remoteFilePath, FtpRemoteExists.Overwrite, true);
                    LoggerService.Log($"Fichier transféré : {file} -> {remoteFilePath}");
                }
                catch (Exception ex)
                {
                    LoggerService.Log($"Erreur lors du transfert : {file} -> {remoteFilePath}, Erreur : {ex.Message}");
                    throw;
                }
            }
        }


        public void Disconnect()
        {
            if (_ftpClient != null && _ftpClient.IsConnected)
            {
                _ftpClient.Disconnect();
                _ftpClient.Dispose();
                _ftpClient = null;
            }
        }


        public List<string> GetRemoteDirectories(string remotePath)
        {
            if (_ftpClient == null || !_ftpClient.IsConnected)
                throw new Exception("FTP non connecté.");

            var directories = new List<string>();
            foreach (var item in _ftpClient.GetListing(remotePath))
            {
                if (item.Type == FtpObjectType.Directory)
                {
                    directories.Add(item.FullName);
                }
            }

            return directories;
        }
    }
}
