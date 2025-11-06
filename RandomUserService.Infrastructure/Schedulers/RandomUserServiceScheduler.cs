using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RandomUserService.Domain.Repositories.Interfaces;
using RandomUserService.Infrastructure.Clients.Interfaces;
using RandomUserService.Infrastructure.Configuration.Providers.Interfaces;
using RandomUserService.Infrastructure.Schedulers.Interfaces;

namespace RandomUserService.Infrastructure.Schedulers
{
    public class RandomUserServiceScheduler : BackgroundService, IRandomUserServiceScheduler
    {
        private readonly ILogger _logger;
        private readonly IServiceScopeFactory _scopeFactory;

        private bool _paused;

        public RandomUserServiceScheduler(ILogger logger, IServiceScopeFactory scopeFactory)
        {
            _logger = logger;
            _scopeFactory = scopeFactory;
        }

        public bool IsRunning => !_paused;

        public string GetStatus()
        {
            switch (IsRunning)
            {
                case true:
                    return "Status: Running.";
                case false:
                    return "Status: Stopped.";
            }
        }

        public void Pause()
        {
            _paused = true;
            _logger.LogInformation("Scheduler paused.");
        }

        public void Resume()
        {
            _paused = false;
            _logger.LogInformation("Scheduler resumed.");
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Scheduler started.");

            using var scope = _scopeFactory.CreateScope();

            var schedulerConfig = scope.ServiceProvider.GetRequiredService<ISchedulerConfigurationProvider>();
            var randomUserClient = scope.ServiceProvider.GetRequiredService<IRandomUserClient>();
            var userRepository = scope.ServiceProvider.GetRequiredService<IUserRepository>();

            var delay = TimeSpan.FromSeconds(schedulerConfig.GetSchedulerInterval());

            while (!stoppingToken.IsCancellationRequested)
            {
                if (!_paused)
                {
                    try
                    {
                        var user = await randomUserClient.GetRandomUserAsync();
                        await userRepository.AddAsync(user);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error fetching/saving user.");
                    }

                }

                await Task.Delay(delay, stoppingToken);
            }            

            _logger.LogInformation("Scheduler stopped.");
        }
    }
}
