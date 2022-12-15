using Document.Repositories;
using System.ComponentModel.DataAnnotations;
using static Duende.IdentityServer.Models.IdentityResources;

namespace Document.Models
{
    public class CompanyModel : IEntity
    {
        [Key]
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string TradeName { get; set; }
        public string Phone { get; set; }
        public string Type { get; set; }
        public Guid? LocationID { get; set; }
        public virtual LocationModel? LocationModel { get; set; }


        public void Copy(CreateUpdateCompany createUpdateCompany)
        {
            Name = createUpdateCompany.Name;
            TradeName = createUpdateCompany.TradeName;
            Phone = createUpdateCompany.Phone;
            Type = createUpdateCompany.Type;
            LocationID = createUpdateCompany.LocationID;
        }
    }
}
