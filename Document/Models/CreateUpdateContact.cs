namespace Document.Models
{
    public class CreateUpdateContact
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public Guid? LocationID { get; set; }


    }
}
