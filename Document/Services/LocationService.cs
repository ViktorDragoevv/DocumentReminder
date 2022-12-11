using Document.Models;
using Document.Repositories;

namespace Document.Services
{
    public class LocationService : ILocationService
    {
        private readonly ILocationRepository _locationRepository;

        public LocationService(ILocationRepository locationRepository)
        {
            _locationRepository = locationRepository;
        }

        public async Task<IEnumerable<ViewLocation>> GetAllLocations()
        {
            var locationModel = await _locationRepository.GetAllAsync();
            var locations = locationModel.Select(x => x.ToModel());
            return locations;
        }
    }
}
