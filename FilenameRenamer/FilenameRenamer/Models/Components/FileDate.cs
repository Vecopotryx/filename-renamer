using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FilenameRenamer.Models.Interfaces;

namespace FilenameRenamer.Models.Components
{
    public class FileDate : IComponentItem
    {
        public string Content { get => "Last Modified Date"; set => throw new NotImplementedException(); }

        public string GetStuff(FileInfo inputFile)
        {
            return inputFile.LastWriteTime.ToShortDateString();
        }
    }
}
