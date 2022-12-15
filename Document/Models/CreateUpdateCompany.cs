namespace Document.Models
{
    public class CreateUpdateCompany
    {
        public string Name { get; set; }
        public string TradeName { get; set; }
        public string Phone { get; set; }
        public string Type { get; set; }
        public Guid? LocationID { get; set; }
    }
}
