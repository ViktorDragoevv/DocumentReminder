using Document.Repositories;
using System.ComponentModel.DataAnnotations;
using static Duende.IdentityServer.Models.IdentityResources;

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

        public virtual ICollection<ContactsModel>? Contacts { get; set; }
        public virtual ICollection<CompanyModel>? Company { get; set; }
        public virtual ICollection<DocumentModel>? Documents { get; set; }

        public LocationModel()
        {
            Contacts = new HashSet<ContactsModel>();
            Company = new HashSet<CompanyModel>();
        }
        public void Copy(ViewLocation locationView)
        {
            Name = locationView.Name;
            Code = locationView.Code;
            Address = locationView.Address;
            City = locationView.City;
        }

    }
}
