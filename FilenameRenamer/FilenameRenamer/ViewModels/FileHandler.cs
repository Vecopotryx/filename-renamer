using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilenameRenamer.ViewModels
{
    class FileHandler
    {
        public void Thing()
        {
            string sourceFile = @"C:\Yes.txt";

            System.IO.FileInfo currentFile = new System.IO.FileInfo(sourceFile);
        }
    }
}
