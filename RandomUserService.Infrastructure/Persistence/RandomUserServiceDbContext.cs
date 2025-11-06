using Microsoft.EntityFrameworkCore;
using RandomUserService.Domain.Entities;
using RandomUserService.Infrastructure.Persistence.Configuration;

namespace RandomUserService.Infrastructure.Persistence
{
    public class RandomUserServiceDbContext : DbContext
    {
        public RandomUserServiceDbContext(DbContextOptions options) : base(options)
        {            
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserConfiguration).Assembly);
        }
    }
}
