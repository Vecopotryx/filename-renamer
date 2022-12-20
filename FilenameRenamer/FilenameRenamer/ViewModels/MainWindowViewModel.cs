using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using FilenameRenamer.Models;
using FilenameRenamer.Models.Components;
using FilenameRenamer.Models.Interfaces;
using FilenameRenamer.Services;
using FilenameRenamer.Views;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;

namespace FilenameRenamer.ViewModels;

public partial class MainWindowViewModel : ObservableObject
{
    // Add graphical preview window that shows a list of files that are about to be renamed with an arrow pointing to new name and then prompts user to confirm?
    // Maybe add option to change folder names?

    private readonly FileHandler fileHandler = new();
    private readonly RenameService renameService = new();

    public ObservableCollection<DirectoryItem> GraphicalFileList
    {
        get => fileHandler.DirectoryItems;
        set => fileHandler.DirectoryItems = value;
    }

    public ObservableCollection<IComponentItem> ComponentItems { get; set; } = new() { new CurrentName() };

    [ObservableProperty] private string _customTextBox = "";

    [ObservableProperty] private object? _selectedObject;

    [ObservableProperty] private bool _currentlyWorking;


    [RelayCommand]
    async Task OpenFolder()
    {
        var dialog = new OpenFolderDialog();
        var result = await dialog.ShowAsync(new MainWindow());

        if (result != null)
        {
            fileHandler.AddNewDirectoryItem(new DirectoryInfo(@result));
        }
    }

    [RelayCommand]
    async Task SelectFile()
    {
        var dialog = new OpenFileDialog();
        var result = await dialog.ShowAsync(new MainWindow());

        if (result != null)
        {
            fileHandler.AddSingleFileToDirectoryItems(new FileInfo(result[0]));
        }
    }

    [RelayCommand]
    void StartRenaming() => Task.Run(() =>
    {
        CurrentlyWorking = true;
        renameService.ExecuteRename(ComponentItems, fileHandler.DirectoryItems);
        CurrentlyWorking = false;
        // Update names in list after rename or discard?
    });

    [RelayCommand]
    void RemoveComponent(object? component)
    {
        if (component is IComponentItem itemToRemove)
        {
            ComponentItems.Remove(itemToRemove);
        }
    }

    [RelayCommand]
    void DiscardSelected()
    {
        if (_selectedObject != null)
        {
            fileHandler.RemoveFromList(_selectedObject);
        }
    }

    // Should the application prompt user first perhaps?
    [RelayCommand]
    void DiscardAll() => fileHandler.DirectoryItems.Clear();

    [RelayCommand]
    void AddCurrentFilename() => ComponentItems.Add(new CurrentName());

    [RelayCommand]
    void AddLastModifiedDate() => ComponentItems.Add(new FileDate());

    [RelayCommand]
    void AddCustomText()
    {
        if (_customTextBox.Length != 0)
        {
            ComponentItems.Add(new Text(_customTextBox));
        }
    }

    [RelayCommand]
    void ClearNewName() => ComponentItems.Clear();
}
