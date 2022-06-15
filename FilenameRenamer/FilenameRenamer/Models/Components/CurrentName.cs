using FilenameRenamer.Models.Interfaces;
using System;
using System.IO;

namespace FilenameRenamer.Models.Components
{
    public class CurrentName : IComponentItem
    {
        public string Content { get => "Current Name"; set => throw new NotImplementedException(); }

        public string GetStuff(FileInfo inputFile) => Path.GetFileNameWithoutExtension(inputFile.Name);
       
    }
}
