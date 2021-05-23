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
        public async Task ExecuteRename(string newName, IEnumerable<DirectoryItem> directoryItems, string path, bool useCustomPath)
        {
            // Need to prevent user from trying to name all files to the same thing, maybe add (1), (2), ... , (n) to the end if files are about to be named the same thing?
            // Also show error if FileInfos is null?
            foreach (var directory in directoryItems)
            {
                if (directory.FileInfos != null)
                {
                    foreach (var file in directory.FileInfos)
                    {
                        HandleRename(file, newName, path, useCustomPath);
                    }
                }
            }
        }

        public void HandleRename(FileInfo inputFile, string newName, string path, bool useCustomPath)
        {
            string localNewName = newName;
            // Check if $currentName$ exists and if so replace it
            localNewName = localNewName.Replace("$currentName$", Path.GetFileNameWithoutExtension(inputFile.Name));
            // Check if $lastModifiedDate$ exists and if so replace it
            localNewName = localNewName.Replace("$lastModifiedDate$", Path.GetFileNameWithoutExtension(inputFile.LastWriteTime.ToShortDateString()));

            try
            {
                if (useCustomPath)
                {
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(@path);
                    }
                    System.Diagnostics.Debug.WriteLine(@path + "\\" + localNewName + inputFile.Extension);
                    inputFile.CopyTo(@path + "\\" + localNewName + inputFile.Extension);
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine(@inputFile.DirectoryName + "\\" + localNewName + inputFile.Extension);
                    inputFile.MoveTo(@inputFile.DirectoryName + "\\" + localNewName + inputFile.Extension);
                }
            }
            catch(Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Some error occurred " + e.Message);
            }
        }
    }
}
