using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RandomUserService.Domain.Repositories.Interfaces;
using RandomUserService.Infrastructure.Clients;
using RandomUserService.Infrastructure.Clients.Interfaces;
using RandomUserService.Infrastructure.Persistence;
using RandomUserService.Infrastructure.Repositories;
using RandomUserService.Infrastructure.Schedulers;
using RandomUserService.Infrastructure.Schedulers.Interfaces;

namespace RandomUserService.Infrastructure.Extensions
{
    public static class InfrastructureExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("RandomUserServiceDb");

            services.AddDbContext<RandomUserServiceDbContext>(options => options.UseSqlServer(connectionString));
            services.AddClients();
            services.AddRepositories();
            services.AddSchedulers();

            return services;
        }

        private static IServiceCollection AddClients(this IServiceCollection services)
        {
            services.AddScoped<IRandomUserClient, RandomUserClient>();

            return services;
        }
        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
        private static IServiceCollection AddSchedulers(this IServiceCollection services)
        {
            services.AddSingleton<IRandomUserServiceScheduler, RandomUserServiceScheduler>();
            services.AddHostedService(provider => (RandomUserServiceScheduler)provider.GetRequiredService<IRandomUserServiceScheduler>());

            return services;
        }
    }
}
