﻿using System;
using System.Collections.Generic;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using FilenameRenamer.Models;
using FilenameRenamer.Models.Components;
using FilenameRenamer.Models.Interfaces;
using FilenameRenamer.Services;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using Avalonia.Platform.Storage;
using CommunityToolkit.Mvvm.Input;

namespace FilenameRenamer.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    // Add graphical preview window that shows a list of files that are about to be renamed with an arrow pointing to new name and then prompts user to confirm?
    // Maybe add option to change folder names?

    private readonly FileHandler _fileHandler = new();
    private readonly RenameService _renameService = new();

    public ObservableCollection<DirectoryItem> GraphicalFileList
    {
        get => _fileHandler.DirectoryItems;
        set => _fileHandler.DirectoryItems = value;
    }

    public ObservableCollection<IComponentItem> ComponentItems { get; set; } = new() { new CurrentName() };
    
    [ObservableProperty] private bool _currentlyWorking;

    public void HandleDroppedFiles(IEnumerable<string>? paths)
    {
        if (paths == null) return;
        foreach (var path in paths)
        {
            if (File.GetAttributes(path).HasFlag(FileAttributes.Directory))
            {
                _fileHandler.AddNewDirectoryItem(new DirectoryInfo(path));
            }
            else
            {
                _fileHandler.AddSingleFileToDirectoryItems(new FileInfo(path));
            }
        }
    }

    [RelayCommand]
    private async Task SelectFolder(/*Window window*/)
    {
        var options = new FolderPickerOpenOptions() { AllowMultiple = true };
        var window = new Window(); // Not ideal to create new Window here. Fix later
        var folders = await window.StorageProvider.OpenFolderPickerAsync(options);
        foreach (var folder in folders)
        {
            if (folder.Path.IsAbsoluteUri)
            {
                 _fileHandler.AddNewDirectoryItem(new DirectoryInfo(folder.Path.LocalPath));
            }
        }
    }

    [RelayCommand]
    private async Task SelectFiles(/*Window window*/)
    {
        var options = new FilePickerOpenOptions() { AllowMultiple = true };
        var window = new Window(); // Not ideal to create new Window here. Fix later
        var files = await window.StorageProvider.OpenFilePickerAsync(options);
        foreach (var file in files)
        {
            if (file.Path.IsAbsoluteUri)
            {
                 _fileHandler.AddSingleFileToDirectoryItems(new FileInfo(file.Path.LocalPath));
            }
        }
    }

    [RelayCommand]
    private void StartRenaming() => Task.Run(() =>
    {
        CurrentlyWorking = true;
        _renameService.ExecuteRename(ComponentItems, _fileHandler.DirectoryItems);
        CurrentlyWorking = false;
        // Update names in list after rename or discard?
    });

    [RelayCommand]
    private void RemoveComponent(object? component)
    {
        if (component is IComponentItem itemToRemove)
        {
            ComponentItems.Remove(itemToRemove);
        }
    }

    [RelayCommand]
    private void DiscardSelected(object? selectedObject)
    {
        if (selectedObject != null)
        {
            _fileHandler.RemoveFromList(selectedObject);
        }
    }

    // Should the application prompt user first perhaps?
    [RelayCommand]
    private void DiscardAll() => _fileHandler.DirectoryItems.Clear();

    [RelayCommand]
    private void AddCurrentFilename() => ComponentItems.Add(new CurrentName());

    [RelayCommand]
    private void AddLastModifiedDate(string format) => ComponentItems.Add(new FileDate(format));

    [RelayCommand]
    private void AddCustomText(string text)
    {
        if (text.Length != 0)
        {
            ComponentItems.Add(new Text(text));
        }
    }

    [RelayCommand]
    private void ClearNewName() => ComponentItems.Clear();
}