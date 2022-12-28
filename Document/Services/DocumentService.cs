using Document.Models;
using Document.Repositories;

namespace Document.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly IDocumentRepository _documentRepository;
        private readonly INotifyService _notifyService;
        private readonly IFileService _fileService;

        public DocumentService(IDocumentRepository documentRepository, INotifyService notifyService, IFileService fileService)
        {
            _documentRepository = documentRepository;
            _notifyService = notifyService;
            _fileService = fileService;
        }

        public async Task<ViewDocument> CreateDocument(CreateUpdateDocumentcs document)
        {
            var documentEntity = document.ToEntity(Guid.NewGuid());
            var newDocument = await _documentRepository.AddAsync(documentEntity);
            if (document.fileID != null)
            {
                await _fileService.UpdateFile((Guid)document.fileID, newDocument.ID);
            }
            var createdDocument = await _documentRepository.GetAllDocumentsWithViewByID(newDocument.ID);
            return createdDocument.ToModel();
        }

        public async Task<ViewDocument> CreateDocumentWithNotifications(CreateUpdateDocumentcs document, IEnumerable<CreateUpdateNotify> createUpdateNotifies)
        {
            var documentEntity = document.ToEntity(Guid.NewGuid());
            var newDocument = await _documentRepository.AddAsync(documentEntity);


            foreach (var notify in createUpdateNotifies)
            {
                notify.DocumentID = newDocument.ID;
                await _notifyService.CreateNotify(notify);
            }
            if (document.fileID != null)
            {
                await _fileService.UpdateFile((Guid)document.fileID, newDocument.ID);
            }
            /*foreach(var file in formFiles)
            {
                await _fileService.CreateFile(file);
            }*/


            var createdDocument = await _documentRepository.GetAllDocumentsWithViewByID(newDocument.ID);
            return createdDocument.ToModel();
        }

        public async Task<IEnumerable<ViewDocument>> GetAllDocuments()
        {
            var documentsModels = await _documentRepository.GetAllDocumentsWithViews();
            var documentsList = documentsModels.Select(x => x.ToModel());
            return documentsList;
        }

        public async Task<ViewDocument> UpdateDocumentByID(CreateUpdateDocumentcs document, Guid id)
        {

            var existingDocument = await _documentRepository.GetByIdAsync(id);
            if (existingDocument == null) { throw new ArgumentNullException("Document not exsist", nameof(CreateUpdateContact)); }
            existingDocument.Copy(document);
            var documentModel = await _documentRepository.UpdateAsync(existingDocument);

            if (document.fileID != null)
            {
                await _fileService.UpdateFile((Guid)document.fileID, existingDocument.ID);
            }

            var updatedDocument = await _documentRepository.GetAllDocumentsWithViewByID(id);
            var toMNodel = updatedDocument.ToModel();
            return toMNodel;
        }

        public async Task<ViewDocument> UpdateDocumentByIDWithNotify(CreateUpdateDocumentcs document, Guid id, IEnumerable<CreateUpdateNotify>? createUpdateNotifies)
        {


            var existingDocument = await _documentRepository.GetByIdAsync(id);
            if (existingDocument == null) { throw new ArgumentNullException("Document not exsist", nameof(CreateUpdateContact)); }
            existingDocument.Copy(document);
            var documentModel = await _documentRepository.UpdateAsync(existingDocument);

            if (document.fileID != null)
            {
                await _fileService.UpdateFile((Guid)document.fileID, existingDocument.ID);
            }
            if (createUpdateNotifies != null)
            {
                foreach (var notify in createUpdateNotifies)
                {
                    notify.DocumentID = existingDocument.ID;
                    await _notifyService.UpdateNotifyByID(notify, notify.ID);
                }
            }

            var updatedDocument = await _documentRepository.GetAllDocumentsWithViewByID(id);
            var toMNodel = updatedDocument.ToModel();
            return toMNodel;

        }
    }
}
