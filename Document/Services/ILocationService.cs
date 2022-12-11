using Document.Models;

namespace Document.Services
{
    public interface ILocationService
    {
        Task<IEnumerable<ViewLocation>> GetAllLocations();
    }
}
