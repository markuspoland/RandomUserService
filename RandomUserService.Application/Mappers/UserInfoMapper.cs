using RandomUserService.Application.DTO;
using RandomUserService.Domain.Entities;
using RandomUserService.Domain.ValueObjects;

namespace RandomUserService.Application.Mappers
{
    public static class UserInfoMapper
    {
        public static UserInfoDto ToUserInfo(this User user)
        {
            return new UserInfoDto
            {
                Gender = user.Gender,
                FirstName = user.Name.First,
                LastName = user.Name.Last,
                Email = user.Email,
                Id = new Id
                {
                    Name = string.IsNullOrEmpty(user.ExternalId.Name) ? "" : user.ExternalId.Name,
                    Value = string.IsNullOrEmpty(user.ExternalId.Value) ? "" : user.ExternalId.Value
                },
                Title = user.Name.Title,
                CreatedAt = user.CreatedAt
            };
        }

        public static DetailedUserInfoDto ToDetailedUserInfo(this User user)
        {
            return new DetailedUserInfoDto
            {
                Gender = user.Gender,
                Name = user.Name,
                Location = user.Location,
                Email = user.Email,
                DateOfBirth = user.DateOfBirth,
                Registered = user.Registered,
                Phone = user.Phone,
                Cell = user.Cell,
                Id = new Id
                {
                    Name = string.IsNullOrEmpty(user.ExternalId.Name) ? "" : user.ExternalId.Name,
                    Value = string.IsNullOrEmpty(user.ExternalId.Value) ? "" : user.ExternalId.Value
                },
                Picture = user.Picture,
                Nat = user.Nat
            };
        }
    }
}
