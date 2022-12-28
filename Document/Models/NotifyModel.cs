using Document.Repositories;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;
using System.Xml.Linq;

namespace Document.Models
{
    public class NotifyModel : IEntity
    {
        [Key]
        public Guid ID { get; set; }
        public int Days { get; set; }
        public bool? Send { get; set; }
        public Guid? DocumentID { get; set; }
        public virtual DocumentModel? DocumentModel { get; set; }
        public Guid? ContactID { get; set; }
        public virtual ContactsModel? ContactModel { get; set; }

        public void Copy(CreateUpdateNotify createUpdateNotify)
        {
            ID = createUpdateNotify.ID;
            Days = createUpdateNotify.Days;
            Send = createUpdateNotify.Send;
            ContactID = createUpdateNotify.ContactID;
            DocumentID = createUpdateNotify.DocumentID;
        }

    }
}
