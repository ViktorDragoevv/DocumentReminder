using Document.Data;
using Document.Models;
using Microsoft.EntityFrameworkCore;

namespace Document.Repositories
{
    public class DocumentRepository : Repository<DocumentModel>, IDocumentRepository
    {
        public DocumentRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<DocumentModel> GetAllDocumentsWithViewByID(Guid id)
        {
            return await Context.DocumentModels
                .Include(x => x.LocationModel)
                .Include(x => x.CategoryModel)
                .Include(x => x.ContactsModel)
                .Include(x => x.Files)
                .Include(x => x.NotifyModels)
                .Include(x => x.CompanyModel)
                .FirstOrDefaultAsync(x => x.ID == id);
        }

        public async Task<IEnumerable<DocumentModel>> GetAllDocumentsWithViews()
        {
            return await Context.DocumentModels
                .Include(x => x.LocationModel)
                .Include(x => x.CategoryModel)
                .Include(x => x.ContactsModel)
                .Include(x => x.Files)
                .Include(x => x.NotifyModels)
                .Include(x => x.CompanyModel).ToListAsync();
        }
    }
}
