using Document.Models;
using Document.Repositories;

namespace Document.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;

        public CompanyService(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public async Task<ViewCompany> AddCompany(CreateUpdateCompany createUpdateCompany)
        {
            var companyEntity = createUpdateCompany.ToEntity(Guid.NewGuid());
            var newCompany = await _companyRepository.AddAsync(companyEntity);
            var createdContact = await _companyRepository.GetCompanyWithLocationByIdAsync(newCompany.ID);
            return createdContact.ToModel();
        }

        public async Task<ViewCompany> EditCompany(CreateUpdateCompany createUpdateCompany, Guid id)
        {
            var existingCompany = await _companyRepository.GetByIdAsync(id);
            if (existingCompany == null) { throw new ArgumentNullException("Contact not exsisting", nameof(CreateUpdateContact)); }
            existingCompany.Copy(createUpdateCompany);
            var contactModel = await _companyRepository.UpdateAsync(existingCompany);
            var updatedContact = await _companyRepository.GetCompanyWithLocationByIdAsync(id);
            var toMNodel = updatedContact.ToModel();
            return toMNodel;
        }

        public async Task<IEnumerable<ViewCompany>> GetAllCompanies()
        {
            var companyModels = await _companyRepository.GetAllCompanyWithLocations();
            //contactsModels.ToList().ForEach(x => x.ToModel());
            //var contactsList = contactsModels.ToList();
            var companyList = companyModels.Select(x => x.ToModel());
            return companyList;
        }

    }
}
