using Document.Models;

namespace Document.Repositories
{
    public interface ICompanyRepository : IRepository<CompanyModel>
    {
        Task<IEnumerable<CompanyModel>> GetAllCompanyWithLocations();
        Task<CompanyModel> GetCompanyWithLocationByIdAsync(Guid id);
    }
}
