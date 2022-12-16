namespace Document.Models
{
    public class ViewDocument
    {
        public Guid ID { get; set; }
        public ViewCategory? ViewCategory { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public virtual IEnumerable<ViewFiles>? Files { get; set; }
        public ViewContact? ViewContact { get; set; }
        public ViewLocation? ViewLocation { get; set; }
        public ViewCompany? ViewCompany { get; set; }
        public string Comments { get; set; }
        public DateTime ExpirationDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public virtual IEnumerable<ViewNotify>? Notify { get; set; }
    }
}
