using System;
using System.IO;
using FilenameRenamer.Models.Interfaces;

namespace FilenameRenamer.Models.Components
{
    public class FileDate : IComponentItem
    {
        public string TextContent { get => "Last modified date"; set => throw new NotImplementedException(); }
        public string GetContent(FileInfo inputFile) => inputFile.LastWriteTime.ToString("yyyy-MM-dd");
    }
}