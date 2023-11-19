using System.Collections.ObjectModel;
using System.IO;
using FilenameRenamer.Models.Interfaces;

namespace FilenameRenamer.Models
{
    public class DirectoryItem : IDirectoryItem
    {
        public ObservableCollection<FileInfo> FileInfos { get; set; }
        public string DirectoryName { get; set; }
        
        public string FullPath { get; set; }
    }
}
