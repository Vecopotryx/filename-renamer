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
        set
        {
            fileHandler.DirectoryItems = value;
        }
    }

    public ObservableCollection<IComponentItem> ComponentItems { get; set; } = new ObservableCollection<IComponentItem>()
        {
            new CurrentName()
        };

    [ObservableProperty]
    private string _customTextBox = "";

    [ObservableProperty]
    private bool _findAndReplaceOn;

    [ObservableProperty]
    private object? _selectedObject;

    [ObservableProperty]
    private bool _currentlyWorking;

    public async Task OpenFolder()
    {
        var dialog = new OpenFolderDialog();
        var result = await dialog.ShowAsync(new MainWindow());

        if (result != null)
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
    
    public void RemoveComponent(object? component)
    {
        if (component is IComponentItem itemToRemove)
        {
            ComponentItems.Remove(itemToRemove);
        }
    }
    public void DiscardSelected()
    {
        if (_selectedObject != null)
        {
            fileHandler.RemoveFromList(_selectedObject);
        }
    }
    // Should the application prompt user first perhaps?
    public void DiscardAll() => fileHandler.DirectoryItems.Clear();

    public void AddCurrentFilename() => ComponentItems.Add(new CurrentName());

    public void AddLastModifiedDate() => ComponentItems.Add(new FileDate());

    public void AddCustomText()
    {
        if (_customTextBox.Length != 0)
        {
            ComponentItems.Add(new Text(_customTextBox));
        }
    }
    public void ClearNewName() => ComponentItems.Clear();

}
