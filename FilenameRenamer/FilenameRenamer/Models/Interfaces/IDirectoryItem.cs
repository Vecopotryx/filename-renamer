using System.Collections.ObjectModel;
using System.IO;

namespace FilenameRenamer.Models.Interfaces
{
    public interface IDirectoryItem
    {
        ObservableCollection<FileInfo> FileInfos { get; set; }
        string DirectoryName { get; set; }
    }
}