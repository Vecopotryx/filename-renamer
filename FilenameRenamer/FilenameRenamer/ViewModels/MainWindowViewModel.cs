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

        private string _newName = "";
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

        private ObservableCollection<string> _graphicalFileList = new ObservableCollection<string>();
        public ObservableCollection<string> GraphicalFileList
        {
            get => _graphicalFileList;
            set
            {
                _graphicalFileList = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(GraphicalFileList)));
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

            
            // MyItems.AddRange(Directory.GetFiles(@result).ToList());

            /*
            Name += String.Join(",",Directory.GetFiles(@result));

            for (int thing = 0; thing < Directory.GetFiles(@result).Length; thing++)
            {
                // MyItems.Append(new FileInfo(Directory.GetFiles(result)[thing]).LastWriteTime.ToString());
                // Name += MyItems.ToString();
                MyItems.Add(Directory.GetFiles(@result)[thing]);
                // Console.WriteLine(MyItems);
                System.Diagnostics.Debug.WriteLine(Directory.GetFiles(@result)[thing]);

            }
            */
            fileHandler.Files = new System.IO.DirectoryInfo(@result).GetFiles();

            foreach (var file in new System.IO.DirectoryInfo(@result).GetFiles())
            {
                GraphicalFileList.Add(file.Name);
            }

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

            foreach (var file in result)
            {
                GraphicalFileList.Add(Path.GetFileName(file));
            }
        }

        public void DiscardFile()
        {
            // Need to remove it from actual FileList, not just graphical.
            System.Diagnostics.Debug.WriteLine("Removed " + _selectedFile + " from list");
            GraphicalFileList.Remove(_selectedFile);
        }

        public void DiscardAll()
        {
            // Should the application prompt user first perhaps?
            GraphicalFileList.Clear();
            Array.Clear(fileHandler.Files,0, fileHandler.Files.Length);
        } 


        public event PropertyChangedEventHandler PropertyChanged;

        /*protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }*/

        public void ApplyButtonClick() => fileHandler.ExecuteRename(NewName);
        public void AddCurrentFilename() => NewName += " $currentName$";
        public void AddLastModifiedDate() => NewName += " $lastModifiedDate$";
    }
}
