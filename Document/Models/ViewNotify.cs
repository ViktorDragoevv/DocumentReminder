namespace Document.Models
{
    public class ViewNotify
    {
        public Guid ID { get; set; }
        public int Days { get; set; }
        public bool Send { get; set; }
        public virtual ViewContact? ContactModel { get; set; }

    }
}
