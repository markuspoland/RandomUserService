using MediatR;
using RandomUserService.Application.DTO;

namespace RandomUserService.Application.Queries.User.GetUserQuery
{
    public class GetUserQuery : IRequest<DetailedUserInfoDto>
    {
        public GetUserQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}
