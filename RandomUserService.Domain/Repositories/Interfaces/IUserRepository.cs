using RandomUserService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RandomUserService.Domain.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task AddAsync(User user);
        Task<IEnumerable<User>> GetAsync();
        Task<User> GetOrDefaultAsync(Guid id);
    }
}
