namespace Document.Models
{
    public class CreateUpdateDocumentcs
    {
        public Guid ID { get; set; }
        public Guid? CategoryID { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public Guid? ContactID { get; set; }
        public Guid? LocationID { get; set; }
        public Guid? CompanyID { get; set; }
        public string Comments { get; set; }
        public DateTime ExpirationDate { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
