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

        private bool _findAndReplaceOn = false;
        public bool FindAndReplaceOn
        {
            get => _findAndReplaceOn;
            set
            {
                _findAndReplaceOn = value;
                OnPropertyChanged();
            }
        }

        public string _textToFind { get; set; }
        public string _textToReplaceWith { get; set; }


        private Object _selectedObject;
        public Object SelectedObject
        {
            get => _selectedObject;
            set
            {
                _selectedObject = value;
                // OnPropertyChanged();
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

            // System.Diagnostics.Debug.WriteLine("Folder selected " + result);
        }

        private string _customPath = "";
        public string CustomPath
        {
            get => _customPath;
            set
            {
                _customPath = value;
                OnPropertyChanged();
            }
        }


        public async Task SelectCopyFolder()
        {
            var dialog = new OpenFolderDialog();
            var result = await dialog.ShowAsync(new MainWindow());

            CustomPath = result;
        }

        public async Task SelectFile()
        {
            var dialog = new OpenFileDialog();
            var result = await dialog.ShowAsync(new MainWindow());

            if (result != null)
            {
                fileHandler.AddSingleFileToDirectoryItems(new FileInfo(result[0]));
            }

        }

        public void DiscardSelected() => fileHandler.RemoveFromList(_selectedObject);

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
            await renameService.ExecuteRename(GetNameString(), fileHandler.DirectoryItems, CustomPath, CopyFilesOptionOn, FindAndReplaceOn);
            CurrentlyWorking = false;
        });

        public void AddCurrentFilename() => ComponentItems.Add(new VarComponent
        {
            content = "Current Name",
            IsEnabled = true
        });

        private int index = 0;
        public void AddLastModifiedDate()
        {
            ComponentItems.Add(new VarComponent
            {
                content = "Last Modified Date" + index,
                IsEnabled = true
            });
            index++;
        }
        public void ClearNewName() => ComponentItems.Clear();

        public ObservableCollection<VarComponent> ComponentItems { get; set; } = new ObservableCollection<VarComponent>()
        {
            new VarComponent(){ content = "Current Name", IsEnabled=true}
        };

        public void MoveComponent(VarComponent component)
        {
            int currentIndex = ComponentItems.IndexOf(component);
            if(currentIndex + 1 < ComponentItems.Count)
            {
                ComponentItems.Move(currentIndex, currentIndex + 1);
            }
        }

        public string GetNameString()
        {
            string name = "";
            foreach(VarComponent component in ComponentItems)
            {
                if(component.content == "Current Name")
                {
                    if(name.Length > 0)
                    {
                        name += " ";
                    }
                    name += "$currentName$";
                } else if (component.content.Contains("Last Modified Date"))
                {
                    if (name.Length > 0)
                    {
                        name += " ";
                    }
                    name += "$lastModifiedDate$";
                }
            }
            System.Diagnostics.Debug.WriteLine(name);
            return name;
        }
    }

    public class VarComponent
    {
        // Implement interfaces and stuff later and move to Models
        public string content { get; set; }
        public bool IsEnabled { get; set; }

    }
}
