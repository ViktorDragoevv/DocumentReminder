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

        public async Task<LocationModel> CreateLocation(ViewLocation location)
        {
            //var contactEntity = location.ToEntity(Guid.NewGuid());
            return await _locationRepository.AddAsync(location.ToEntity(new Guid()));
        }

        public async Task<LocationModel> DeleteLocation(Guid id)
        {
            return await _locationRepository.RemoveAsync(id);
        }

        public async Task<IEnumerable<ViewLocation>> GetAllLocations()
        {
            var locationModel = await _locationRepository.GetAllAsync();
            var locations = locationModel.Select(x => x.ToModel());
            return locations;
        }

        public async Task<LocationModel> UpdateLocation(ViewLocation location, Guid id)
        {
            var updatedLocation = await _locationRepository.GetByIdAsync(id);
            if (updatedLocation == null) { throw new ArgumentNullException("Location not exsisting", nameof(CreateUpdateContact)); }
            updatedLocation.Copy(location);
            return await _locationRepository.UpdateAsync(updatedLocation);
        }
    }
}
