using MediatR;
using RandomUserService.Application.DTO;
using RandomUserService.Application.Mappers;
using RandomUserService.Domain.Repositories.Interfaces;

namespace RandomUserService.Application.Queries.User.GetUsersQuery
{
    public class GetUsersQueryHandler(IUserRepository userRepository) : IRequestHandler<GetUsersQuery, IEnumerable<UserInfoDto>>
    {
        public async Task<IEnumerable<UserInfoDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await userRepository.GetAsync();

            return users.Select(user => user.ToUserInfo());
        }
    }
}
