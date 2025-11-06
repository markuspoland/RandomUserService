using RandomUserService.Domain.ValueObjects.Converters;
using RandomUserService.Domain.ValueObjects.SubValueObjects;
using System.Text.Json.Serialization;

namespace RandomUserService.Domain.ValueObjects
{
    public class Location
    {
        public Street Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        [JsonConverter(typeof(PostcodeConverter))]
        public string? PostCode { get; set; }
        public Coordinates Coordinates { get; set; }
        public TimeZone TimeZone { get; set; }
    }
}
