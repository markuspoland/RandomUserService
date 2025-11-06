using MediatR;
using RandomUserService.Application.DTO;

namespace RandomUserService.Application.Queries.User.GetUsersQuery
{
    public class GetUsersQuery : IRequest<IEnumerable<UserInfoDto>>
    {
        public GetUsersQuery()
        {
        }
    }
}
