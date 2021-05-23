using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reactive;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Controls;
using DynamicData;
using FilenameRenamer.Views;
using FilenameRenamer.Models;
using FilenameRenamer.Services;
using ReactiveUI;

namespace FilenameRenamer.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        // Add graphical preview window that shows a list of files that are about to be renamed with an arrow pointing to new name and then prompts user to confirm?
        // Maybe add option to change folder names?

        private FileHandler fileHandler = new FileHandler();
        private RenameService renameService = new RenameService(); 

        public ObservableCollection<DirectoryItem> GraphicalFileList
        {
            get => fileHandler.DirectoryItems;
            set
            {
                fileHandler.DirectoryItems = value;
                OnPropertyChanged();
            }
        }

        private string _newName = "$currentName$";
        public string NewName
        {
            get => _newName;
            set
            {
                // Maybe trim value for preview?
                _newName = value;
                OnPropertyChanged();
            }
        }


        private bool _copyFilesOptionOn = false;
        public bool CopyFilesOptionOn
        {
            get => _copyFilesOptionOn;
            set
            {
                _copyFilesOptionOn = value;
                OnPropertyChanged();
            }
        }

        private string _selectedFile;
        public string SelectedFile
        {
            get => _selectedFile;
            set
            {
                _selectedFile = value;
                OnPropertyChanged();
            }
        }

        private bool _currentlyWorking;
        public bool CurrentlyWorking
        {
            get => _currentlyWorking;
            set
            {
                _currentlyWorking = value;
                OnPropertyChanged();
            }
        }

        public async Task OpenFolder()
        {
            var dialog = new OpenFolderDialog();
            var result = await dialog.ShowAsync(new MainWindow());

            fileHandler.AddNewDirectoryItem(new System.IO.DirectoryInfo(@result));

            System.Diagnostics.Debug.WriteLine("Folder selected " + result);
        }

        public async Task SelectFile()
        {
            var dialog = new OpenFileDialog();
            var result = await dialog.ShowAsync(new MainWindow());

            var resultFile = new FileInfo(result[0]);

            fileHandler.AddSingleFileToDirectoryItems(resultFile);
        }

        public void DiscardFile() => fileHandler.RemoveFileFromList(new FileInfo(_selectedFile));

        public void DiscardAll()
        {
            // Should the application prompt user first perhaps?
            fileHandler.DirectoryItems.Clear();
        }
        
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void ApplyButtonClick() => Task.Run(async () =>
        {
            CurrentlyWorking = true;
            await renameService.ExecuteRename(NewName, fileHandler.DirectoryItems);
            CurrentlyWorking = false;
        });

        public void AddCurrentFilename() => NewName += " $currentName$";
        public void AddLastModifiedDate() => NewName += " $lastModifiedDate$";
        public void ClearNewName() => NewName = "";
    }
}
