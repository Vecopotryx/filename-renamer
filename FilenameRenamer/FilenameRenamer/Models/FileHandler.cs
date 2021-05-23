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

        private bool _currentlyWorking = false;
        public bool CurrentlyWorking
        {
            get => _currentlyWorking;
            set
            {
                _currentlyWorking = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentlyWorking)));
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
