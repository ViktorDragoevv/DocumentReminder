using Document.Models;

namespace Document.Services
{
    public interface IDocumentService
    {
        Task<IEnumerable<ViewDocument>> GetAllDocuments();
        Task<ViewDocument> CreateDocument(CreateUpdateDocumentcs document);
        Task<ViewDocument> UpdateDocumentByID(CreateUpdateDocumentcs contact, Guid id);
    }
}
