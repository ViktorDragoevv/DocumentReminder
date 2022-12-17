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

        public async Task<ViewDocument> CreateDocument(CreateUpdateDocumentcs document)
        {
            var documentEntity = document.ToEntity(Guid.NewGuid());
            var newDocument = await _documentRepository.AddAsync(documentEntity);
            var createdDocument = await _documentRepository.GetAllDocumentsWithViewByID(newDocument.ID);
            return createdDocument.ToModel();
        }

        public async Task<IEnumerable<ViewDocument>> GetAllDocuments()
        {
            var documentsModels =  await _documentRepository.GetAllDocumentsWithViews();
            var documentsList = documentsModels.Select(x => x.ToModel());
            return documentsList;
        }

        public async Task<ViewDocument> UpdateDocumentByID(CreateUpdateDocumentcs document, Guid id)
        {
            var existingDocument = await _documentRepository.GetByIdAsync(id);
            if (existingDocument == null) { throw new ArgumentNullException("Document not exsist", nameof(CreateUpdateContact)); }
            existingDocument.Copy(document);
            var documentModel = await _documentRepository.UpdateAsync(existingDocument);
            var updatedDocument = await _documentRepository.GetAllDocumentsWithViewByID(id);
            var toMNodel = updatedDocument.ToModel();
            return toMNodel;
        }
    }
}
