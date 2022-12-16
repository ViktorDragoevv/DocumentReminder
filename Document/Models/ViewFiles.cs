namespace Document.Models
{
    public class ViewFiles
    {
        public Guid ID { get; set; }
        public byte[] Content { get; set; }
        public string FileName { get; set; }
    }
}
