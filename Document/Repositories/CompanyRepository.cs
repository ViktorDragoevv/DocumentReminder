using Document.Data;
using Document.Models;
using Microsoft.EntityFrameworkCore;

namespace Document.Repositories
{
    public sealed class CompanyRepository : Repository<CompanyModel>, ICompanyRepository
    {
        public CompanyRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<CompanyModel>> GetAllCompanyWithLocations()
        {
            return await Context.CompanyModel.Include(x => x.LocationModel).ToListAsync();
        }

        public Task<CompanyModel> GetCompanyWithLocationByIdAsync(Guid id)
        {
            return Context.CompanyModel
                .Include(x => x.LocationModel)
                .FirstOrDefaultAsync(x => x.ID == id);
        }
    }
}
