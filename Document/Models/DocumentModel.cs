using Document.Repositories;
using NuGet.Packaging.Signing;
using System.ComponentModel.DataAnnotations;
using static Duende.IdentityServer.Models.IdentityResources;

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


        public void Copy(CreateUpdateDocumentcs createUpdateDocument)
        {
            ID = createUpdateDocument.ID;
            CategoryID = createUpdateDocument.CategoryID;
            Status = createUpdateDocument.Status;
            Name = createUpdateDocument.Name;
            LocationID = createUpdateDocument.LocationID;
            ContactID = createUpdateDocument.ContactID;
            CompanyID = createUpdateDocument.CompanyID;
            Comments = createUpdateDocument.Comments;
            ExpirationDate = createUpdateDocument.ExpirationDate;
            CreatedDate = createUpdateDocument.CreatedDate;
        }

    }
}
