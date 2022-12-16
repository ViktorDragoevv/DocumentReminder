using Document.Repositories;
using NuGet.Packaging.Signing;
using System.ComponentModel.DataAnnotations;

namespace Document.Models
{
    public class DocumentModel : IEntity
    {
        [Key]
        public Guid ID { get; set; }
        public Guid? CategoryID { get; set; }
        public virtual CategoryModel? CategoryModel { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public virtual ICollection<Files>? Files { get; set; }
        public Guid? ContactID { get; set; }
        public virtual ContactsModel? ContactsModel { get; set; }
        public Guid? LocationID { get; set; }
        public virtual LocationModel? LocationModel { get; set; }
        public Guid? CompanyID { get; set; }
        public virtual CompanyModel? CompanyModel { get; set; }
        public string Comments { get; set; }
        public DateTime ExpirationDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public virtual ICollection<NotifyModel>? NotifyModels { get; set; }




    }
}
