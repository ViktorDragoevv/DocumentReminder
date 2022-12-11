using Document.Data;
using Document.Models;

namespace Document.Repositories
{
    public class LocationRepository : Repository<LocationModel>, ILocationRepository
    {
        public LocationRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
