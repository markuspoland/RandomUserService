using Microsoft.Extensions.DependencyInjection;

namespace RandomUserService.Application.Extensions
{
    public static class ApplicationExtensions
    {
        public static IServiceCollection AddHandlers(this IServiceCollection services)
        {
            var assembly = typeof(ApplicationExtensions).Assembly;

            services.AddMediatR(c => c.RegisterServicesFromAssembly(assembly));

            return services;
        }
    }
}
