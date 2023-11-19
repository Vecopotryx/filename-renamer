using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

namespace FilenameRenamer.Models
{
    class FileHandler
    {
        public ObservableCollection<DirectoryItem> DirectoryItems { get; set; } = new();

        public void AddNewDirectoryItem(DirectoryInfo directoryInfo)
        {
            DirectoryItems.Add(new DirectoryItem
            {
                DirectoryName = directoryInfo.Name,
                FullPath = directoryInfo.FullName,
                FileInfos = new ObservableCollection<FileInfo>(new List<FileInfo>(directoryInfo.GetFiles()
                    .Where(file => (file.Attributes & FileAttributes.Hidden) == 0)))
            });
        }

        private void RemoveFileFromList(FileInfo file)
        {
            foreach (var directory in DirectoryItems.ToList())
            {
                if (file.Directory == null || directory.DirectoryName != file.Directory.Name) continue;
                if (directory.FileInfos.Count <= 1)
                {
                    DirectoryItems.Remove(directory);
                }
                else
                {
                    directory.FileInfos.Remove(file);
                }
            }
        }

        public void RemoveFromList(object input)
        {
            switch (input)
            {
                case FileInfo file:
                    RemoveFileFromList(file);
                    break;
                case DirectoryItem item:
                    DirectoryItems.Remove(item);
                    break;
            }
        }

        public void AddSingleFileToDirectoryItems(FileInfo file)
        {
            bool directoryAlreadyExists = false;
            foreach (var directoryItem in DirectoryItems)
            {
                if (file.Directory == null || directoryItem.DirectoryName != file.Directory.Name) continue;
                directoryItem.FileInfos.Add(file);
                directoryAlreadyExists = true;
                break;
            }

            if (!directoryAlreadyExists)
            {
                DirectoryItems.Add(new DirectoryItem
                {
                    DirectoryName = file.Directory != null ? file.Directory.Name : "",
                    FileInfos = new ObservableCollection<FileInfo>() { file }
                });
            }
        }
    }
}
