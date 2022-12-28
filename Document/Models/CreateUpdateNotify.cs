namespace Document.Models
{
    public class CreateUpdateNotify
    {
        public Guid ID { get; set; }
        public int Days { get; set; }
        public bool Send { get; set; } = false;
        public Guid? ContactID { get; set; }
        public Guid? DocumentID { get; set; }
    }
}
