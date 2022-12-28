namespace Document.Models
{
    public class DocumentWithNotifications
    {
        public CreateUpdateDocumentcs CreateUpdateDocumentcs { get; set; }
        public IEnumerable<CreateUpdateNotify> createUpdateNotifies { get; set; }
    }
}
