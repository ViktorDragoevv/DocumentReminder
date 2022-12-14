using Document.Models;

namespace Document.Services
{
    public interface ILocationService
    {
        Task<IEnumerable<ViewLocation>> GetAllLocations();
        Task<LocationModel> CreateLocation(ViewLocation location);
        Task<LocationModel> UpdateLocation(ViewLocation location, Guid id);
        Task<LocationModel> DeleteLocation(Guid id);
    }
}
