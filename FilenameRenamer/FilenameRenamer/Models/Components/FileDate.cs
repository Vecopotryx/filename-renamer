using System;
using System.Collections.Generic;
using System.IO;
using Avalonia.Controls;
using FilenameRenamer.Models.Interfaces;

namespace FilenameRenamer.Models.Components
{
    public class FileDate : IComponentItem
    {
        public string TextContent { get => "Last modified date"; set => throw new NotImplementedException(); }
        public string GetContent(FileInfo inputFile) => inputFile.LastWriteTime.ToShortDateString();
    }
}