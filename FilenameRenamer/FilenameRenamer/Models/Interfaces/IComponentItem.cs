using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilenameRenamer.Models.Interfaces
{
    public interface IComponentItem
    {
        public Avalonia.Controls.Control Component { get; set; }
        public string GetContent(System.IO.FileInfo inputFile);
    }
}
