using System;
using System.IO;
using FilenameRenamer.Models.Interfaces;

namespace FilenameRenamer.Models.Components
{
    public class CurrentName : IComponentItem
    {        
        public string TextContent { get => "Current name"; set => throw new NotImplementedException(); }
        public string GetContent(FileInfo inputFile) => Path.GetFileNameWithoutExtension(inputFile.Name);

    }
}
