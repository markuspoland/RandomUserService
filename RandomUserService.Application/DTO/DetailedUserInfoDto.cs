using RandomUserService.Domain.ValueObjects;
using RandomUserService.Domain.ValueObjects.SubValueObjects;

namespace RandomUserService.Application.DTO
{
    public class DetailedUserInfoDto
    {
        public string Gender { get; set; }
        public Name Name { get; set; }
        public Location Location { get; set; }
        public string Email { get; set; }
        public DateOfBirth DateOfBirth { get; set; }
        public Registered Registered { get; set; }
        public string Phone { get; set; }
        public string Cell { get; set; }
        public Id Id { get; set; }
        public Picture Picture { get; set; }
        public string Nat { get; set; }
    }
}
