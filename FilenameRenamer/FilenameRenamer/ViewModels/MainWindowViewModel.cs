using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reactive;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Controls;
using FilenameRenamer.Views;
using ReactiveUI;

namespace FilenameRenamer.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged

    {
        // private FileHandler fileHandler = new FileHandler();

        // private string _newName  = "Default";

        private string _name = "no";

        public List<string> myItems = Directory.GetFiles(@"C:\").ToList();

            // { "Test1", "Test2", "Test3", "Test4" };


        public List<string> MyItems
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

        public async Task OpenFolder()
        {
            var dialog = new OpenFolderDialog();
            var result = await dialog.ShowAsync(new MainWindow());

            
            /*if (result != null)
            {
                await OpenFold(result);
            }
            Trace.WriteLine("DIR IS: " + result);
            */
            MyItems.AddRange(Directory.GetFiles(@result).ToList());
            Name += new FileInfo(Directory.GetFiles(result)[1]).LastWriteTime;
            Name += result;
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
