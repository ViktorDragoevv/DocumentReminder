using Document.Models;

namespace Document.Services
{
    public interface IDocumentService
    {
        Task<IEnumerable<ViewDocument>> GetAllDocuments();
        Task<ViewDocument> CreateDocument(CreateUpdateDocumentcs document);
        Task<ViewDocument> UpdateDocumentByID(CreateUpdateDocumentcs contact, Guid id);
        Task<ViewDocument> UpdateDocumentByIDWithNotify(CreateUpdateDocumentcs contact, Guid id, IEnumerable<CreateUpdateNotify>? createUpdateNotifies);

        Task<ViewDocument> CreateDocumentWithNotifications(CreateUpdateDocumentcs document, IEnumerable<CreateUpdateNotify> createUpdateNotifies);
    }
}
