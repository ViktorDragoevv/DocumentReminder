using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.ComponentModel.DataAnnotations;

namespace Document.Models
{
    public class ContactsModel
    {
        [Key]
        [Required]
        public Guid ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public Guid? LocationID { get; set; }

        public virtual LocationModel LocationModel  { get; set;}
    }
}
