using Document.Data;
using Document.Models;

namespace Document.Repositories
{
    public class FileRepository : Repository<Files>, IFileRepository
    {
        public FileRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
