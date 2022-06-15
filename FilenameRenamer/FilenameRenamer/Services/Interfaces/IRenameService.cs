using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using FilenameRenamer.Models;

namespace FilenameRenamer.Services.Interfaces
{
    internal interface IRenameService
    {
        void ExecuteRename(ObservableCollection<Models.Interfaces.IComponentItem> componentItems, IEnumerable<DirectoryItem> directoryItems);
        void HandleRename(ObservableCollection<Models.Interfaces.IComponentItem> componentItems, FileInfo inputFile);
    }
}