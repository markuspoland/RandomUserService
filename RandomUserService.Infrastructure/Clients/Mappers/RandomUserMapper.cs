using RandomUserService.Domain.Entities;
using RandomUserService.Infrastructure.Clients.DTO;

namespace RandomUserService.Infrastructure.Clients.Mappers
{
    public static class RandomUserMapper
    {
        public static User? ToDomainUser(this RandomUserDto userDto)
        {
            if (userDto is not null)
                return new User(
                    Guid.NewGuid(),
                    userDto.Gender,
                    userDto.Name,
                    userDto.Location,
                    userDto.Email,
                    userDto.Dob,
                    userDto.Registered,
                    userDto.Phone,
                    userDto.Cell,
                    userDto.Id,
                    userDto.Picture,
                    userDto.Nat,
                    DateTime.Now);

            return null;

        }
    }
}
