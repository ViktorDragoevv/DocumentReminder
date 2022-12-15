namespace Document.Models
{
    public class ViewCompany
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string TradeName { get; set; }
        public string Phone { get; set; }
        public string Type { get; set; }
        public ViewLocation? ViewLocation { get; set; }
    }
}
