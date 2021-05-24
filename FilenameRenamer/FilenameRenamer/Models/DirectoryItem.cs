using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FilenameRenamer.Models.Interfaces;

namespace FilenameRenamer.Models
{
    public class DirectoryItem : IDirectoryItem
    {
        public ObservableCollection<FileInfo> FileInfos { get; set; }
        public string DirectoryName { get; set; }
    }
}
