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
using FilenameRenamer.Models.Interfaces;
using FilenameRenamer.Models.Components;

namespace FilenameRenamer.ViewModels
{
    public class MainWindowViewModel : ReactiveObject
    {
        // Add graphical preview window that shows a list of files that are about to be renamed with an arrow pointing to new name and then prompts user to confirm?
        // Maybe add option to change folder names?

        private readonly FileHandler fileHandler = new();
        private readonly RenameService renameService = new();

        public ObservableCollection<DirectoryItem> GraphicalFileList
        {
            get => fileHandler.DirectoryItems;
            set
            {
                fileHandler.DirectoryItems = value;
            }
        }

        public ObservableCollection<IComponentItem> ComponentItems { get; set; } = new ObservableCollection<IComponentItem>()
        {
            new CurrentName()
        };

        private bool _findAndReplaceOn;
        public bool FindAndReplaceOn
        {
            get => _findAndReplaceOn;
            set => this.RaiseAndSetIfChanged(ref _findAndReplaceOn, value);
        }

        private object? _selectedObject;
        public object? SelectedObject
        {
            get => _selectedObject;
            set => _selectedObject = value;
        }

        private bool _currentlyWorking;
        public bool CurrentlyWorking
        {
            get => _currentlyWorking;
            set => this.RaiseAndSetIfChanged(ref _currentlyWorking, value);
        }

        public async Task OpenFolder()
        {
            var dialog = new OpenFolderDialog();
            var result = await dialog.ShowAsync(new MainWindow());

            if(result != null)
            {
                fileHandler.AddNewDirectoryItem(new DirectoryInfo(@result));
            }
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

        public void ApplyButtonClick() => Task.Run(() =>
        {
            CurrentlyWorking = true;
            renameService.ExecuteRename(ComponentItems, fileHandler.DirectoryItems);
            CurrentlyWorking = false;
            // Update names in list after rename or discard?
        });

        public void MoveComponent(IComponentItem component)
        {
            int currentIndex = ComponentItems.IndexOf(component);
            if (currentIndex + 1 < ComponentItems.Count)
            {
                ComponentItems.Move(currentIndex, currentIndex + 1);
            }
        }

        public void RemoveComponent(IComponentItem component) => ComponentItems.Remove(component);

        public void DiscardSelected()
        {
            if(_selectedObject != null)
            {
                fileHandler.RemoveFromList(_selectedObject);
            }
        }
        // Should the application prompt user first perhaps?
        public void DiscardAll() => fileHandler.DirectoryItems.Clear();

        public void AddCurrentFilename() => ComponentItems.Add(new CurrentName());

        public void AddLastModifiedDate() => ComponentItems.Add(new FileDate());

        public void AddCustomText() => ComponentItems.Add(new Text());

        public void ClearNewName() => ComponentItems.Clear();
        
    }
}
