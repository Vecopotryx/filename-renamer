using System;
using System.IO;
using FilenameRenamer.Models.Interfaces;

namespace FilenameRenamer.Models.Components
{
    public class Text : IComponentItem
    {
        readonly string text = "";
        public string TextContent { get => text; set => throw new NotImplementedException(); }

        public string GetContent(FileInfo inputFile) => text;
    }
}