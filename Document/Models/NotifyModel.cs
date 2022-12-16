using Document.Repositories;
using System.ComponentModel.DataAnnotations;

namespace Document.Models
{
    public class NotifyModel : IEntity
    {
        [Key]
        public Guid ID { get; set; }
        public int Days { get; set; }
        public bool Send { get; set; }
        public Guid? DocumentID { get; set; }
        public virtual DocumentModel? DocumentModel { get; set; }
        public Guid? ContactID { get; set; }
        public virtual ContactsModel? ContactModel { get; set; }


    }
}
