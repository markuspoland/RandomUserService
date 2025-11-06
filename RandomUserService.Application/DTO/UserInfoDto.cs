using RandomUserService.Domain.ValueObjects;

namespace RandomUserService.Application.DTO
{
    public class UserInfoDto
    {
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public Id Id { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
