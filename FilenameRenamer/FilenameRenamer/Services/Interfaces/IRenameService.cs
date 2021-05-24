using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using FilenameRenamer.Models;

namespace FilenameRenamer.Services.Interfaces
{
    internal interface IRenameService
    {
        Task ExecuteRename(string newName, IEnumerable<DirectoryItem> directoryItems, string path, bool useCustomPath, bool useFindAndReplace);
        void HandleRename(FileInfo inputFile, string newName, string path, bool useCustomPath);
    }
}