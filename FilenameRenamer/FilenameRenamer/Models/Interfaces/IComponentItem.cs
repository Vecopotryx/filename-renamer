using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilenameRenamer.Models.Interfaces
{
    public interface IComponentItem
    {
        public string Content { get; set; }
        public string GetStuff(System.IO.FileInfo inputFile);
    }
}
