using System;
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

public partial class MainWindowViewModel : ObservableObject
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

    [ObservableProperty] private string _customTextBox = "";

    [ObservableProperty] private object? _selectedObject;

    [ObservableProperty] private bool _currentlyWorking;


    [RelayCommand]
    private async Task SelectFolder(Window window)
    {
        var folder = await window.StorageProvider.OpenFolderPickerAsync(new FolderPickerOpenOptions());
        if (folder[0].TryGetUri(out Uri? path))
        {
            _fileHandler.AddNewDirectoryItem(new DirectoryInfo(path.OriginalString));
        }
    }

    [RelayCommand]
    private async Task SelectFiles(Window window)
    {
        var files = await window.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions()
            { AllowMultiple = true });
        foreach (var file in files)
        {
            if (file.TryGetUri(out Uri? path))
            {
                _fileHandler.AddSingleFileToDirectoryItems(new FileInfo(path.OriginalString));
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
    private void DiscardSelected()
    {
        if (_selectedObject != null)
        {
            _fileHandler.RemoveFromList(_selectedObject);
        }
    }

    // Should the application prompt user first perhaps?
    [RelayCommand]
    private void DiscardAll() => _fileHandler.DirectoryItems.Clear();

    [RelayCommand]
    private void AddCurrentFilename() => ComponentItems.Add(new CurrentName());

    [RelayCommand]
    private void AddLastModifiedDate() => ComponentItems.Add(new FileDate());

    [RelayCommand]
    private void AddCustomText()
    {
        if (_customTextBox.Length != 0)
        {
            ComponentItems.Add(new Text(_customTextBox));
        }
    }

    [RelayCommand]
    private void ClearNewName() => ComponentItems.Clear();
}