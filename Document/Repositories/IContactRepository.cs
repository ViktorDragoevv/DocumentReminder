using Document.Models;

namespace Document.Repositories
{
    public interface IContactRepository : IRepository<ContactsModel>
    {
        Task<IEnumerable<ContactsModel>> GetAllContactsWithLocation();

        Task<ContactsModel> GetContactWithLocationByIdAsync(Guid id);
    }

}
