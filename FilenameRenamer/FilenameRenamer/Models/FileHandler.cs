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
                FileInfos = new ObservableCollection<FileInfo>(new List<FileInfo>(directoryInfo.GetFiles()))
            });
        }

        public void RemoveFileFromList(FileInfo file)
        {
            foreach (var directory in DirectoryItems.ToList())
            {
                if (directory.DirectoryName == file.Directory.Name)
                {
                    foreach (var fileInfo in directory.FileInfos.ToList())
                    {
                        if (fileInfo.Name == file.Name)
                        {
                            if (directory.FileInfos.Count <= 1)
                            {
                                DirectoryItems.Remove(directory);
                            }
                            else
                            {
                                directory.FileInfos.Remove(fileInfo);
                            }
                        }
                    }
                }
            }
        }

        public void AddSingleFileToDirectoryItems(FileInfo file)
        {
            bool directoryAlreadyExists = false;
            foreach (var directoryItem in DirectoryItems)
            {
                if (directoryItem.DirectoryName == file.Directory.Name)
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
                    DirectoryName = file.Directory.Name,
                    FileInfos = new ObservableCollection<FileInfo>() { file }
                });
            }
        }
    }
}
