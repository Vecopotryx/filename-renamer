using System;
using System.IO;
using FilenameRenamer.Models.Interfaces;

namespace FilenameRenamer.Models.Components
{
    public class CurrentName : IComponentItem
    {

        Avalonia.Controls.Button btn = new Avalonia.Controls.Button() { Content="Current Name" };
        
        public Avalonia.Controls.Control Component { get => btn; set => throw new NotImplementedException(); }

        public string GetContent(FileInfo inputFile) => Path.GetFileNameWithoutExtension(inputFile.Name);
       
    }
}
