using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using FilenameRenamer.ViewModels;

namespace FilenameRenamer.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel() {Name = "Hello"};

#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
