using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilenameRenamer.Models
{
    class FileHandler
    {
        public System.IO.FileInfo[] Files { get; set; }

        public ObservableCollection<DirectoryItem> DirectoryItems { get; set; } = new ObservableCollection<DirectoryItem>();

        public void HandleRename(FileInfo inputFile, string newName)
        {
            string localNewName = newName;
            // Check if $currentName$ exists and if so replace it
            // Should probably be able to remove the if-statement
            if (newName.Contains("$currentName$"))
            {
                localNewName = localNewName.Replace("$currentName$", Path.GetFileNameWithoutExtension(inputFile.Name));
            }
            // Check if $lastModifiedDate$ exists and if so replace it
            if (newName.Contains("$lastModifiedDate$"))
            {
                localNewName = localNewName.Replace("$lastModifiedDate$", Path.GetFileNameWithoutExtension(inputFile.LastWriteTime.ToShortDateString()));
            }
            System.Diagnostics.Debug.WriteLine("{0} would have been renamed to {1}", inputFile.Name, localNewName.Trim() + inputFile.Extension);
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
        public void ExecuteRename(string newName)
        {
            // Need to prevent user from trying to name all files to the same thing, maybe add (1), (2), ... , (n) to the end if files are about to be named the same thing?
            // Also show error if Files is null?
            if (Files != null)
            {
                foreach (var file in Files)
                {
                    HandleRename(file, newName);
                }
            }
        }

        public void AddNewDirectoryItem(DirectoryInfo directoryInfo)
        {
            System.Diagnostics.Debug.WriteLine(directoryInfo.Name);
            
            DirectoryItems.Add(new DirectoryItem
            {
                DirectoryName = directoryInfo.Name,
                FileInfos = new ObservableCollection<FileInfo>(new List<FileInfo>(directoryInfo.GetFiles()))
            });
        }
    }
}
