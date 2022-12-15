using Document.Data;
using Document.Models;
using Microsoft.EntityFrameworkCore;

namespace Document.Repositories
{
    public sealed class ContactRepository : Repository<ContactsModel>, IContactRepository
    {

        public ContactRepository(ApplicationDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<ContactsModel>> GetAllContactsWithLocation()
        {
            return await Context.ContactsModel.Include(x => x.LocationModel).ToListAsync();
        }

        public Task<ContactsModel> GetContactWithLocationByIdAsync(Guid id)
        {
            return Context.ContactsModel
                .Include(x => x.LocationModel)
                .FirstOrDefaultAsync(x => x.ID == id);
        }
    }
}
