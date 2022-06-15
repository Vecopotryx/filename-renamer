using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace FilenameRenamer.Views.Components
{
    public partial class Draggable : UserControl, IPanel
    {
        public Draggable()
        {
            InitializeComponent();
        }

        Controls IPanel.Children => throw new System.NotImplementedException();

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
