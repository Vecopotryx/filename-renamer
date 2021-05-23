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
using ReactiveUI;

namespace FilenameRenamer.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        // Add graphical preview window that shows a list of files that are about to be renamed with an arrow pointing to new name and then prompts user to confirm?
        // Maybe add option to change folder names?

        private FileHandler fileHandler = new FileHandler();

        public ObservableCollection<DirectoryItem> GraphicalFileList
        {
            get => fileHandler.DirectoryItems;
            set
            {
                fileHandler.DirectoryItems = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(GraphicalFileList)));
            }
        }

        public MainWindowViewModel()
        {
            this.GraphicalFileList = fileHandler.DirectoryItems;
        }

        private string _newName = "$currentName$";
        public string NewName
        {
            get => _newName;
            set
            {
                // Maybe trim value for preview?
                _newName = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(NewName)));
            }
        }


        private bool _copyFilesOptionOn = false;
        public bool CopyFilesOptionOn
        {
            get => _copyFilesOptionOn;
            set
            {
                _copyFilesOptionOn = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CopyFilesOptionOn)));
            }
        }

        public async Task OpenFolder()
        {
            var dialog = new OpenFolderDialog();
            var result = await dialog.ShowAsync(new MainWindow());

            fileHandler.AddNewDirectoryItem(new System.IO.DirectoryInfo(@result));

            System.Diagnostics.Debug.WriteLine("Folder selected " + result);
        }

        private string _selectedFile;
        public string SelectedFile
        {
            get => _selectedFile;
            set
            {
                _selectedFile = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedFile)));
            }
        }



        public async Task SelectFile()
        {
            var dialog = new OpenFileDialog();
            var result = await dialog.ShowAsync(new MainWindow());

            var resultFile = new FileInfo(result[0]);

            fileHandler.AddSingleFileToDirectoryItems(resultFile);
        }

        public void DiscardFile()
        {
            // Need to remove it from actual FileList, not just graphical.
            // System.Diagnostics.Debug.WriteLine("Removed " + _selectedFile + " from list");
            // GraphicalFileList.Remove(_selectedFile);
            // I've had problems getting SelectedFile to work effeciently. 
            throw new NotImplementedException();
        }

        public void DiscardAll()
        {
            // Should the application prompt user first perhaps?
            fileHandler.DirectoryItems.Clear();
        } 


        public event PropertyChangedEventHandler PropertyChanged;


        /*protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }*/

        public void ApplyButtonClick() => fileHandler.ExecuteRename(NewName);
        public void AddCurrentFilename() => NewName += " $currentName$";
        public void AddLastModifiedDate() => NewName += " $lastModifiedDate$";
        public void ClearNewName() => NewName = "";

    }
}
