using System;
using System.IO;
using FilenameRenamer.Models.Interfaces;

namespace FilenameRenamer.Models.Components
{
    public class Text : IComponentItem
    {
        readonly Avalonia.Controls.TextBox box = new() { Watermark = "Custom text" };

        public Avalonia.Controls.Control Component { get => box; set => throw new NotImplementedException(); }

        public string GetContent(FileInfo inputFile) => box.Text;
    }
}