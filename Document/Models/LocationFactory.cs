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
    }
}
