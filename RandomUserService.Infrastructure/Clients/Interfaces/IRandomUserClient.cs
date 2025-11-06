using RandomUserService.Domain.Entities;

namespace RandomUserService.Infrastructure.Clients.Interfaces
{
    public interface IRandomUserClient
    {
        string Url { get; }
        Task<User> GetRandomUserAsync();
    }
}
