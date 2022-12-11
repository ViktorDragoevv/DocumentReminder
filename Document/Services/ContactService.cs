using Document.Models;
using Document.Repositories;

namespace Document.Services
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _contactRepository;

        public ContactService(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        public async Task<ContactsModel> GetContactByID(Guid id)
        {
            return await _contactRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<ContactsModel>> GetAllContacts()
        {
            return await _contactRepository.GetAllAsync();
        }

        public async Task<ContactsModel> UpdateContactByID(ContactsModel contact)
        {
            return await _contactRepository.UpdateAsync(contact);
        }
    }
}
