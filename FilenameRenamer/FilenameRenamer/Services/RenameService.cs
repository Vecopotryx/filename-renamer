using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FilenameRenamer.Models;
using FilenameRenamer.Services.Interfaces;

namespace FilenameRenamer.Services
{
    class RenameService : IRenameService
    {
        public async Task ExecuteRename(string newName, IEnumerable<DirectoryItem> directoryItems, string path, bool useCustomPath, bool useFindAndReplace)
        {
            // Need to prevent user from trying to name all files to the same thing, maybe add (1), (2), ... , (n) to the end if files are about to be named the same thing?
            // Also show error if FileInfos is null?
            foreach (var directory in directoryItems)
            {
                string newPath = path;
                if (directoryItems.Count() > 1)
                {
                    newPath = path + "/" + directory.DirectoryName;
                }

                if (directory.FileInfos != null)
                {
                    foreach (var file in directory.FileInfos)
                    {
                        HandleRename(file, newName, newPath, useCustomPath);
                    }
                }
            }
        }

        public void HandleRename(FileInfo inputFile, string newName, string path, bool useCustomPath)
        {
            string localNewName = newName.Trim();
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
                    inputFile.CopyTo(@path + "/" + localNewName + inputFile.Extension);
                }
                else
                {
                    inputFile.MoveTo(@inputFile.DirectoryName + "/" + localNewName + inputFile.Extension);
                }
            }
            catch(Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Some error occurred " + e.Message);
            }
        }
    }
}
