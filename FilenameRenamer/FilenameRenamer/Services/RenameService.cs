using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using FilenameRenamer.Models;
using FilenameRenamer.Models.Interfaces;
using FilenameRenamer.Services.Interfaces;

namespace FilenameRenamer.Services
{
    class RenameService : IRenameService
    {
        public void ExecuteRename(ObservableCollection<IComponentItem> componentItems,
            IEnumerable<DirectoryItem> directoryItems)
        {
            foreach (var directory in directoryItems)
            {
                if (directory.FileInfos.Count <= 0) continue;
                foreach (var file in directory.FileInfos)
                {
                    HandleRename(componentItems, file);
                }
            }
        }

        public void HandleRename(ObservableCollection<IComponentItem> componentItems, FileInfo inputFile)
        {
            StringBuilder stringBuilder = new();
            foreach (var component in componentItems)
            {
                stringBuilder.Append($"{component.GetContent(inputFile)} ");
            }

            try
            {
                inputFile.MoveTo(@inputFile.DirectoryName + "/" + stringBuilder.ToString().Trim() +
                                 inputFile.Extension);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Some error occurred " + e.Message);
            }
        }
    }
}