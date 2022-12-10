namespace Document.Models
{
    public class ContactsModel
    {
        public Guid ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public ContactsModel(Guid iD, string firstName, string lastName, string email, string phoneNumber)
        {
            ID = iD;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PhoneNumber = phoneNumber;
        }
    }
}
