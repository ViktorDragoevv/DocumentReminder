using Document.Models;

namespace Document.Services
{
    public interface IContactService
    {
        Task<IEnumerable<ViewContact>> GetAllContacts();
        Task<ContactsModel> GetContactByID(Guid id);
        Task<ContactsModel> UpdateContactByID(CreateUpdateContact contact, Guid id);
        Task<ContactsModel> CreateContact(CreateUpdateContact contact);
        Task<ContactsModel> DeleteContact(Guid id);
    }
}
