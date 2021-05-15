using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Reactive;
using System.Text;
using ReactiveUI;

namespace FilenameRenamer.ViewModels
{
    public class MainWindowViewModel : ViewModelBase

    {
        // private FileHandler fileHandler = new FileHandler();

        private string _newName  = "Default";

        public string NewName
        {
            get => _newName;
            set
            {
                _newName = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(NewName)));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        public void RunTheThing()
        {
            // fileHandler.Thing();
            // Console.WriteLine("Clicked");
            NewName = "Pog";
        }
    }
}
