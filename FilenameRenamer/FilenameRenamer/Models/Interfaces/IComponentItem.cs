namespace FilenameRenamer.Models.Interfaces
{
    public interface IComponentItem
    {
        //public Avalonia.Controls.Control Component { get; set; }
        public string TextContent { get; set; }
        public string GetContent(System.IO.FileInfo inputFile);
    }
}
