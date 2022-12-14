using Document.Models;
using Document.Repositories;
using System.Text.RegularExpressions;

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

        public async Task<IEnumerable<ViewContact>> GetAllContacts()
        {
            var contactsModels =  await _contactRepository.GetAllContactsWithLocation();
            //contactsModels.ToList().ForEach(x => x.ToModel());
            //var contactsList = contactsModels.ToList();
            var contactsList = contactsModels.Select(x => x.ToModel());
            return contactsList;
        }

        public async Task<ContactsModel> UpdateContactByID(CreateUpdateContact contact, Guid id)
        {
            var existingContact = await _contactRepository.GetByIdAsync(id);
            if (existingContact == null) { throw new ArgumentNullException("Contact not exsisting", nameof(CreateUpdateContact)); }
            existingContact.Copy(contact);
            return await _contactRepository.UpdateAsync(existingContact);
        }

        public async Task<ContactsModel> CreateContact(CreateUpdateContact contact)
        {
            var contactEntity = contact.ToEntity(Guid.NewGuid());
            return await _contactRepository.AddAsync(contactEntity);
        }

        public Task<ContactsModel> DeleteContact(Guid id)
        {
            var deleteContact = _contactRepository.RemoveAsync(id);
            return deleteContact;
        }
    }
}
