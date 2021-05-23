using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FilenameRenamer.Models;

namespace FilenameRenamer.Services
{
    class RenameService
    {
        public async Task ExecuteRename(string newName, IEnumerable<DirectoryItem> directoryItems)
        {
            // Need to prevent user from trying to name all files to the same thing, maybe add (1), (2), ... , (n) to the end if files are about to be named the same thing?
            // Also show error if FileInfos is null?
            foreach (var directory in directoryItems)
            {
                if (directory.FileInfos != null)
                {
                    foreach (var file in directory.FileInfos)
                    {
                        HandleRename(file, newName);
                    }
                }
            }
            //CurrentlyWorking = false;
        }

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
    }
}
