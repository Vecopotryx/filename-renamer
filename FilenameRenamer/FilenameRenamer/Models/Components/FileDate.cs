using System;
using System.Collections.Generic;
using System.IO;
using Avalonia.Controls;
using FilenameRenamer.Models.Interfaces;

namespace FilenameRenamer.Models.Components
{
    public class FileDate : IComponentItem
    {
        readonly ComboBox cbx = new()
        {
            Items = new List<ComboBoxItem>() {
                new ComboBoxItem() { Content = "YYYY-MM-DD" },
                new ComboBoxItem() { Content = "HH-mm-ss" }
            },
            SelectedIndex = 0
        };

        public Control Component { get => cbx; set => throw new NotImplementedException(); }

        public string GetContent(FileInfo inputFile)
        {
            return cbx.SelectedIndex switch
            {
                1 => inputFile.LastWriteTime.ToString("HH-mm-ss"),
                _ => inputFile.LastWriteTime.ToShortDateString(),
            };
        }
    }
}