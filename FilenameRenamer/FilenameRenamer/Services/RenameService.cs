using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FilenameRenamer.Models;
using FilenameRenamer.Models.Interfaces;
using FilenameRenamer.Services.Interfaces;

namespace FilenameRenamer.Services
{
    class RenameService : IRenameService
    {
        public void ExecuteRename(ObservableCollection<IComponentItem> componentItems, IEnumerable<DirectoryItem> directoryItems)
        {
            foreach (var directory in directoryItems)
            {
                if(directory.FileInfos.Count > 0)
                {
                    foreach(FileInfo file in directory.FileInfos)
                    {
                        HandleRename(componentItems, file);
                    }
                }
            }
        }

        public void HandleRename(ObservableCollection<IComponentItem> componentItems, FileInfo inputFile)
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach(IComponentItem component in componentItems)
            {
                stringBuilder.Append($"{component.GetStuff(inputFile)} ");
            }

            try
            {
                inputFile.MoveTo(@inputFile.DirectoryName + "/" + stringBuilder.ToString().Trim() + inputFile.Extension);
            } 
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Some error occurred " + e.Message);
            }
        }
    }
}
