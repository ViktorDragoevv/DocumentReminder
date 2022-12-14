namespace Document.Models
{
    public static class LocationFactory
    {
        public static ViewLocation ToModel(this LocationModel location)
        {
            return location == null
                ? null
                : new ViewLocation
                {
                    ID = location.ID,
                    Name = location.Name,
                    Code = location.Code,
                    Address = location.Address,
                    City = location.City,
                };
        }

        public static LocationModel ToEntity(this ViewLocation locationView, Guid contactID)
        {
            return locationView == null
                ? null
                : new LocationModel
                {
                    ID = contactID,
                    Name = locationView.Name,
                    Address = locationView.Address,
                    Code = locationView.Code,
                    City = locationView.City,
                };
        }
    }
}
