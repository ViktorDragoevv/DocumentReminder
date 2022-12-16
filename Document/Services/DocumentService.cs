using Document.Models;
using Document.Repositories;

namespace Document.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly IDocumentRepository _documentRepository;

        public DocumentService(IDocumentRepository documentRepository)
        {
            _documentRepository = documentRepository;
        }

        public async Task<IEnumerable<ViewDocument>> GetAllDocuments()
        {
            var documentsModels =  await _documentRepository.GetAllDocumentsWithViews();
            var documentsList = documentsModels.Select(x => x.ToModel());
            return documentsList;
        }
    }
}
