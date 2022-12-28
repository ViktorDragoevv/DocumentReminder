using Document.Data;
using Document.Models;
using Microsoft.EntityFrameworkCore;

namespace Document.Repositories
{
    public class NotifyRepository : Repository<NotifyModel>, INotifyRepository
    {
        public NotifyRepository(ApplicationDbContext context) : base(context)
        {

        }

        public async Task<NotifyModel> GetAllNotifyWithViewByID(Guid id)
        {
            return await Context.NotifyModels
                    .Include(x => x.ContactID)
                    .Include(x => x.DocumentID)
                .FirstOrDefaultAsync(x => x.ID == id);
        }
    }
}
