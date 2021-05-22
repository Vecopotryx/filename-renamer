using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilenameRenamer.Models
{
    public class DirectoryItem
    {
        public ObservableCollection<FileInfo> FileInfos { get; set; }
        public string DirectoryName { get; set; }
    }
}
