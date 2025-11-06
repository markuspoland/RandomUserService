using MediatR;
using RandomUserService.Application.DTO;
using RandomUserService.Application.Mappers;
using RandomUserService.Domain.Repositories.Interfaces;

namespace RandomUserService.Application.Queries.User.GetUserQuery
{
    public class GetUserQueryHandler(IUserRepository userRepository) : IRequestHandler<GetUserQuery, DetailedUserInfoDto>
    {
        public async Task<DetailedUserInfoDto> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var user = await userRepository.GetOrDefaultAsync(request.Id);

            return user.ToDetailedUserInfo();
        }
    }
}
