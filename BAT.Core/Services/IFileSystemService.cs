namespace BAT.Core.Services
{
    using System.IO;

    public interface IFileSystemService
    {
        public DirectoryInfo GetAccountDirectoryInfo(string accountId);
        public DirectoryInfo GetOrCreateDirectoryInfo(string path);
        public FileInfo GetOrCreateFileInfo(string path);
        public bool WriteToFile(string path, string content);
        public string ReadFromFile(string path);
    }
}
