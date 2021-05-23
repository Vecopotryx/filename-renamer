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
    class FileHandler : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<DirectoryItem> DirectoryItems { get; set; } = new ObservableCollection<DirectoryItem>();

        // Set should be private, just not sure how to get it to update in MainWindow.

        private string _currentProgress = "";
        public string CurrentProgress
        {
            get => _currentProgress;
            set
            {
                _currentProgress = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentProgress)));
            }
        }

        public void HandleRename(FileInfo inputFile, string newName)
        {
            string localNewName = newName;
            // Check if $currentName$ exists and if so replace it
            // Should probably be able to remove the if-statement
            if (newName.Contains("$currentName$"))
            {
                localNewName = localNewName.Replace("$currentName$", Path.GetFileNameWithoutExtension(inputFile.Name));
            }
            // Check if $lastModifiedDate$ exists and if so replace it
            if (newName.Contains("$lastModifiedDate$"))
            {
                localNewName = localNewName.Replace("$lastModifiedDate$", Path.GetFileNameWithoutExtension(inputFile.LastWriteTime.ToShortDateString()));
            }
            // System.Diagnostics.Debug.WriteLine("{0} would have been renamed to {1}", inputFile.Name, localNewName.Trim() + inputFile.Extension);
            CurrentProgress = inputFile.Name + " would have been renamed to " + localNewName.Trim() +
                              inputFile.Extension;
            System.Diagnostics.Debug.WriteLine(CurrentProgress);
            // inputFile.CopyTo(@"C:\Test2" + localNewName + inputFile.Extension);
            /*if (CopyFilesOptionOn)
            {
                inputFile.CopyTo(@"C:\Test2\" + newName);
            }
            else
            {
                inputFile.MoveTo(@"C:\Test2\" + newName);
            }*/
        }

        public async Task ExecuteRename(string newName)
        {
            // Need to prevent user from trying to name all files to the same thing, maybe add (1), (2), ... , (n) to the end if files are about to be named the same thing?
            // Also show error if FileInfos is null?
            foreach (var directory in DirectoryItems)
            {
                if (directory.FileInfos != null)
                {
                    foreach (var file in directory.FileInfos)
                    {
                        HandleRename(file, newName);
                    }
                }
            }
        }

        public void AddNewDirectoryItem(DirectoryInfo directoryInfo)
        {
            System.Diagnostics.Debug.WriteLine(directoryInfo.Name);
            
            DirectoryItems.Add(new DirectoryItem
            {
                DirectoryName = directoryInfo.Name,
                FileInfos = new ObservableCollection<FileInfo>(new List<FileInfo>(directoryInfo.GetFiles()))
            });
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
