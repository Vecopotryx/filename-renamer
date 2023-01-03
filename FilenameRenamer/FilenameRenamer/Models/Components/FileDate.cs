using System;
using System.IO;
using FilenameRenamer.Models.Interfaces;

namespace FilenameRenamer.Models.Components
{
    public class FileDate : IComponentItem
    {
        private readonly string _format = "yyyy-MM-dd";
        public string TextContent { get => "Last modified date (" + _format + ")"; set => throw new NotImplementedException(); }
        public string GetContent(FileInfo inputFile) => inputFile.LastWriteTime.ToString(_format);

        public FileDate(string format)
        {
            _format = format;
        }
    }
}