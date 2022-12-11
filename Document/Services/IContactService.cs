using Document.Models;

namespace Document.Services
{
    public interface IContactService
    {
        Task<IEnumerable<ContactsModel>> GetAllContacts();
        Task<ContactsModel> GetContactByID(Guid id);
        Task<ContactsModel> UpdateContactByID(ContactsModel contact);
    }
}
