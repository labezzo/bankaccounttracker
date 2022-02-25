namespace BAT.Core.Services
{
    using System;
    using System.IO;

    public class FileSystemService : IFileSystemService
    {
        public DirectoryInfo GetAccountDirectoryInfo(string accountId)
        {
            DirectoryInfo directoryInfo = null;

            if (!string.IsNullOrWhiteSpace(accountId))
            {
                var bookingsDir = GetOrCreateDirectoryInfo(Consts.PathBookings);
                if (bookingsDir != null && bookingsDir.Exists)
                {
                    var accountDirectoryPath = Path.Combine(Consts.PathBookings, accountId);
                    var accountDirectory = GetOrCreateDirectoryInfo(accountDirectoryPath);
                    if (accountDirectory.Exists)
                    {
                        directoryInfo = accountDirectory;
                    }
                    else
                    {
                        LogService.LogInfo("accountDirectory does not exist. Path: " + accountDirectoryPath);
                    }
                }
                else
                {
                    LogService.LogInfo("bookingsDir does not exist. Path: " + Consts.PathBookings);
                }
            }
            else
            {
                LogService.LogInfo("accountId is empty");
            }

            return directoryInfo;
        }

        public DirectoryInfo GetOrCreateDirectoryInfo(string path)
        {
            DirectoryInfo directory = null;

            if (!string.IsNullOrWhiteSpace(path))
            {
                directory = new DirectoryInfo(path);
                if (!directory.Exists)
                {
                    directory = Directory.CreateDirectory(path);
                }
            }
            else
            {
                LogService.LogInfo("path is empty");
            }

            return directory;
        }

        public FileInfo GetOrCreateFileInfo(string path)
        {
            FileInfo file = null;

            if (!string.IsNullOrWhiteSpace(path))
            {
                file = new FileInfo(path);
                if (!file.Exists)
                {
                    File.Create(path).Close();
                    file = new FileInfo(path);
                }
            }
            else
            {
                LogService.LogInfo("path is empty");
            }

            return file;
        }

        public bool WriteToFile(string path, string content)
        {
            bool success = false;

            if (!string.IsNullOrWhiteSpace(path) && !string.IsNullOrWhiteSpace(content))
            {
                try
                {
                    File.WriteAllText(path, content);
                    success = true;
                }
                catch (Exception ex)
                {
                    LogService.LogError("error writing to file", ex);
                }
            }

            return success;
        }

        public string ReadFromFile(string path)
        {
            var content = string.Empty;

            if (!string.IsNullOrWhiteSpace(path))
            {
                using (var streamReader = new StreamReader(path))
                {
                    content = streamReader.ReadToEnd();
                }
            }
            else
            {
                LogService.LogInfo("path is empty");
            }

            return content;
        }
    }
}
