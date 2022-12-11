namespace Document.Models
{
    public class ViewContact
    {
        public Guid ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public ViewLocation? ViewLocation { get; set; }
    }
}
