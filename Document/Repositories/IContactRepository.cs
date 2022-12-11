using Document.Models;

namespace Document.Repositories
{
    public interface IContactRepository : IRepository<ContactsModel>
    {
        Task<IEnumerable<ContactsModel>> GetAllContactsWithLocation();
    }

}
