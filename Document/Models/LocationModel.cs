using Document.Repositories;
using System.ComponentModel.DataAnnotations;

namespace Document.Models
{
    public class LocationModel : IEntity
    {
        [Key]
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Address { get; set; }
        public string City { get; set; }

        public virtual ICollection<ContactsModel> Contacts { get; set; }

        public LocationModel()
        {
            Contacts = new HashSet<ContactsModel>();
        }

    }
}
