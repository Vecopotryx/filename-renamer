using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilenameRenamer.Models
{
    class DirectoryItem
    {
        public List<FileInfo> FileInfos { get; set; }
        public string DirectoryName { get; set; }
    }
}
