using Document.Models;

namespace Document.Repositories
{
    public interface IDocumentRepository : IRepository<DocumentModel>
    {
        Task<IEnumerable<DocumentModel>> GetAllDocumentsWithViews();
    }
}
