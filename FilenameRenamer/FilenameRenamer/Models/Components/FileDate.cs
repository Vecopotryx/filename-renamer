using System;
using System.Collections.Generic;
using System.IO;
using Avalonia.Controls;
using FilenameRenamer.Models.Interfaces;

namespace FilenameRenamer.Models.Components
{
    public class FileDate : IComponentItem
    {

        ComboBox cbx = new ComboBox() { Items = new List<ComboBoxItem>() { new ComboBoxItem() { Content = "YYYY-MM-DD" }, new ComboBoxItem() { Content = "HH-mm-ss" }  }, SelectedIndex = 0 };
        public Control Component { get => cbx; set => throw new NotImplementedException(); }

        public string GetContent(FileInfo inputFile)
        {
            switch (cbx.SelectedIndex)
            {
                default:
                case 0: return inputFile.LastWriteTime.ToShortDateString();
                case 1: return inputFile.LastWriteTime.ToString("HH-mm-ss");
            }
        }
    }
}