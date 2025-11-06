using RandomUserService.Domain.ValueObjects;
using RandomUserService.Domain.ValueObjects.SubValueObjects;

namespace RandomUserService.Tests.Builders
{
    public class LocationBuilder
    {
        private Street _street = null;
        private string _city = "Moscow";
        private string _state = "";
        private string _country = "Argentina";
        private string _postcode = "12312";
        private Coordinates _coordinates = null;
        private TimeZone _timeZone = null;

        public LocationBuilder WithStreet(int number, string name)
        {
            _street = new Street
            {
                Number = number,
                Name = name
            };

            return this;
        }

        public LocationBuilder WithCoordinates(double latitude, double longitute)
        {
            _coordinates = new Coordinates
            {
                Latitude = latitude,
                Longitude = longitute
            };

            return this;
        }

        public LocationBuilder WithTimezone(string offset, string description)
        {
            _timeZone = new TimeZone
            {
                Offset = offset,
                Description = description
            };

            return this;
        }

        public Location Build()
        {
            return new Location()
            {
                Street = _street,
                City = _city,
                State = _state,
                Country = _country,
                Coordinates = _coordinates,
                TimeZone = _timeZone,
                PostCode = _postcode
            };
        }
    }
}
