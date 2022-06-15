using System;
using System.IO;
using FilenameRenamer.Models.Interfaces;

namespace FilenameRenamer.Models.Components
{
    public class FileDate : IComponentItem
    {
        Avalonia.Controls.Button btn = new Avalonia.Controls.Button() { Content = "Last Modified Date" };
        public Avalonia.Controls.Control Component { get => btn; set => throw new NotImplementedException(); }

        public string GetStuff(FileInfo inputFile) => inputFile.LastWriteTime.ToShortDateString();
    }
}