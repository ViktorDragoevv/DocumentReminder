using Document.Models;

namespace Document.Services
{
    public interface ICompanyService
    {
        Task<IEnumerable<ViewCompany>> GetAllCompanies();
        Task<ViewCompany> AddCompany(CreateUpdateCompany createUpdateCompany);

        Task<ViewCompany> EditCompany(CreateUpdateCompany createUpdateCompany, Guid id);
    }
}
