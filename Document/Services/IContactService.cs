using Document.Models;

namespace Document.Services
{
    public interface IContactService
    {
        Task<IEnumerable<ViewContact>> GetAllContacts();
        Task<ContactsModel> GetContactByID(Guid id);
        Task<ViewContact> UpdateContactByID(CreateUpdateContact contact, Guid id);
        Task<ViewContact> CreateContact(CreateUpdateContact contact);
        Task<ContactsModel> DeleteContact(Guid id);
    }
}
