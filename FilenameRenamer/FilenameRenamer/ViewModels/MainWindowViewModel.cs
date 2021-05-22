using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reactive;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Controls;
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

        private List<string> _graphicalFileList = new List<string>();
        public List<string> GraphicalFileList
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
