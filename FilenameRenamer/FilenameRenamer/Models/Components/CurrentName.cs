using FilenameRenamer.Models.Interfaces;
using System;
using System.IO;

namespace FilenameRenamer.Models.Components
{
    public class CurrentName : IComponentItem
    {
        public string Content { get => "Current Name"; set => throw new NotImplementedException(); }
        
        private bool findReplace = false;

        private string textToFind = "";

        private string textToReplaceWith = "";

        public void UpdateFindReplace(bool findReplaceOn, string find, string replace)
        {
            findReplace = findReplaceOn;
            textToFind = find;
            textToReplaceWith = replace;
        }

 
        public string GetStuff(FileInfo inputFile)
        {
            string name = Path.GetFileNameWithoutExtension(inputFile.Name);
            if (findReplace)
            {
                if (textToFind.Contains("$lmd$"))
                {
                    textToFind = textToFind.Replace("$lmd$", inputFile.LastWriteTime.ToShortDateString());
                } 
                
                name = name.Replace(textToFind, textToReplaceWith);
            }

            return name;
        }
    }
}
