using Document.Models;

namespace Document.Services
{
    public interface IDocumentService
    {
        Task<IEnumerable<ViewDocument>> GetAllDocuments();
    }
}
