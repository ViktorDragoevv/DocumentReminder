using Document.Data;
using Document.Models;

namespace Document.Repositories
{
    public sealed class ContactRepository : Repository<ContactsModel>, IContactRepository
    {
        public ContactRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
