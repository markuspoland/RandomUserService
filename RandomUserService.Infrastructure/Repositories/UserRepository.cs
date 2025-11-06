using Microsoft.EntityFrameworkCore;
using RandomUserService.Domain.Entities;
using RandomUserService.Domain.Repositories.Interfaces;
using RandomUserService.Infrastructure.Persistence;

namespace RandomUserService.Infrastructure.Repositories
{
    public class UserRepository(RandomUserServiceDbContext context) : IUserRepository
    {
        public async Task AddAsync(User user)
        {
            await context.AddAsync(user);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<User>> GetAsync()
        {
            return await context.Users.ToListAsync();
        }

        public async Task<User> GetOrDefaultAsync(Guid id)
        {
            return await context.Users
                .AsQueryable()
                .Where(user => user.Id.Equals(id))
                .FirstOrDefaultAsync() ??
                throw new Exception($"User with id: {id.ToString()} not found.");
        }
    }
}
