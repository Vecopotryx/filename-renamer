using Avalonia.Controls;
using Avalonia.Input;
using FilenameRenamer.ViewModels;

namespace FilenameRenamer.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        SetupDrop();
    }

    private void SetupDrop()
    {
        void Drop(object? sender, DragEventArgs e)
        {
            if (e.Data.Contains(DataFormats.FileNames))
            {
                (DataContext as MainWindowViewModel)?.HandleDroppedFiles(e.Data.GetFileNames());
            }
        }

        AddHandler(DragDrop.DropEvent, Drop);
    }
}