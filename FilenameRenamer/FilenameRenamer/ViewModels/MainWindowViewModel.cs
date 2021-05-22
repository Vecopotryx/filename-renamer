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

        private List<string> myItems = Directory.GetFiles(@"C:\").ToList();

        static System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(@"C:\Test");
        static System.IO.FileInfo[] fi = di.GetFiles();

        private void HandleRename(FileInfo inputFile, string newName)
        {
            string localNewName = newName;
            // Check if $currentName$ exists and if so replace it
            if (Name.Contains("$currentName$"))
            {
                localNewName = localNewName.Replace("$currentName$",Path.GetFileNameWithoutExtension(inputFile.Name));
            }
            // Check if $lastModifiedDate$ exists and if so replace it
            if (Name.Contains("$lastModifiedDate$"))
            {
                localNewName = localNewName.Replace("$lastModifiedDate$", Path.GetFileNameWithoutExtension(inputFile.LastWriteTime.ToShortDateString()));
            }
            System.Diagnostics.Debug.WriteLine(localNewName.Trim() + inputFile.Extension);
            // inputFile.CopyTo(@"C:\Test2" + localNewName + inputFile.Extension);
            /*if (CopyFilesOptionOn)
            {
                inputFile.CopyTo(@"C:\Test2\" + newName);
            }
            else
            {
                inputFile.MoveTo(@"C:\Test2\" + newName);
            }*/
        }

        // Add graphical preview window that shows a list of files that are about to be renamed with an arrow pointing to new name and then prompts user to confirm.
        // Maybe add option to change folder names?
        public void ExecuteRename()
        {
            // Need to prevent user from trying to name all files to the same thing, maybe add (1), (2), ... , (n) to the end if files are about to be named the same thing?
            foreach (var file in fi)
            {
                HandleRename(file, Name);
                System.Diagnostics.Debug.WriteLine(file.Name);
                System.Diagnostics.Debug.WriteLine(Path.GetFileNameWithoutExtension(file.Name));
                System.Diagnostics.Debug.WriteLine(file.Extension);
            }
        }

        public List<string> MyItems
        {
            get => myItems;
            set
            {
                myItems = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(MyItems)));
            }
        }

        private bool _copyFilesOptionOn = false;
        public bool CopyFilesOptionOn
        {
            get => _copyFilesOptionOn;
            set
            {
                _copyFilesOptionOn = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CopyFilesOptionOn)));
            }
        }


        public string Name
        {
            get => _name;
            set
            {
                // Maybe trim value for preview?
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
            // MyItems.AddRange(Directory.GetFiles(@result).ToList());

            /*
            Name += String.Join(",",Directory.GetFiles(@result));

            for (int thing = 0; thing < Directory.GetFiles(@result).Length; thing++)
            {
                // MyItems.Append(new FileInfo(Directory.GetFiles(result)[thing]).LastWriteTime.ToString());
                // Name += MyItems.ToString();
                MyItems.Add(Directory.GetFiles(@result)[thing]);
                // Console.WriteLine(MyItems);
                System.Diagnostics.Debug.WriteLine(Directory.GetFiles(@result)[thing]);

            }
            */
             di = new System.IO.DirectoryInfo(@result);
             fi = di.GetFiles();

            System.Diagnostics.Debug.WriteLine("Folder selected " + result);

            // Name += result;
            
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

       // public void RunTheThing() => Name = "Pog";
       public void RunTheThing()
       {
           foreach (var file in fi)
           {
               System.Diagnostics.Debug.WriteLine(file.Name);
               System.Diagnostics.Debug.WriteLine(Path.GetFileNameWithoutExtension(file.Name));
               System.Diagnostics.Debug.WriteLine(file.Extension);
           }
       }
       public void AddCurrentFilename() => Name += " $currentName$";
        public void AddLastModifiedDate() => Name += " $lastModifiedDate$";
    }
}
