using Document.Repositories;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace Document.Models
{
    public class ContactsModel : IEntity
    {
        [Key]
        [Required]
        public Guid ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public Guid? LocationID { get; set; }

        public virtual LocationModel? LocationModel  { get; set;}

        public void Copy(CreateUpdateContact createUpdateContact)
        {
            FirstName = createUpdateContact.FirstName;
            LastName = createUpdateContact.LastName;
            Email = createUpdateContact.Email;
            PhoneNumber = createUpdateContact.PhoneNumber;
            LocationID = createUpdateContact.LocationID;
        }

    }
}
