using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Reactive;
using System.Runtime.CompilerServices;
using System.Text;
using ReactiveUI;

namespace FilenameRenamer.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged

    {
        // private FileHandler fileHandler = new FileHandler();

        // private string _newName  = "Default";

        private string _name = "no";

        public string[] myItems = Directory.GetFiles(@"c:\");

            // { "Test1", "Test2", "Test3", "Test4" };


        public string[] MyItems
        {
            get => myItems;
            set
            {
                myItems = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(MyItems)));
            }
        }

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Name)));
            }
        }
        /*
        public string NewName
        {
            get => _newName;
            set
            {
                _newName = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(NewName)));
            }
        }
        */
        public event PropertyChangedEventHandler PropertyChanged;

        /*protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }*/

        public void RunTheThing() => Name = "Pog";
        public void AddCurrentFilename() => Name += " $currentFilename$";
        public void AddLastModifiedDate() => Name += " $lastModifiedDate$";
    }
}
