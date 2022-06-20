using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilenameRenamer.Models
{
    class FileHandler
    {
        public ObservableCollection<DirectoryItem> DirectoryItems { get; set; } = new ObservableCollection<DirectoryItem>();

        public void AddNewDirectoryItem(DirectoryInfo directoryInfo)
        {
            System.Diagnostics.Debug.WriteLine(directoryInfo.Name);

            DirectoryItems.Add(new DirectoryItem
            {
                DirectoryName = directoryInfo.Name,
                FileInfos = new ObservableCollection<FileInfo>(new List<FileInfo>(directoryInfo.GetFiles().Where(file => (file.Attributes & FileAttributes.Hidden) == 0)))
            });
        }

        private void RemoveFileFromList(FileInfo file)
        {
            foreach (var directory in DirectoryItems.ToList())
            {
                if (file.Directory != null && directory.DirectoryName == file.Directory.Name)
                {
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
        }

        public void RemoveFromList(object input)
        {
            if (input is FileInfo file)
            {
                RemoveFileFromList(file);
            }
            else if (input is DirectoryItem item)
            {
                DirectoryItems.Remove(item);
            }
        }

        public void AddSingleFileToDirectoryItems(FileInfo file)
        {
            bool directoryAlreadyExists = false;
            foreach (var directoryItem in DirectoryItems)
            {
                if (file.Directory != null && directoryItem.DirectoryName == file.Directory.Name)
                {
                    directoryItem.FileInfos.Add(file);
                    directoryAlreadyExists = true;
                    break;
                }
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
