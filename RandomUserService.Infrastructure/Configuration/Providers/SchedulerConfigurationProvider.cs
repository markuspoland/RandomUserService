using RandomUserService.Infrastructure.Configuration.Providers.Interfaces;

namespace RandomUserService.Infrastructure.Configuration.Providers
{
    public class SchedulerConfigurationProvider(InfrastructureConfiguration configuration) : ISchedulerConfigurationProvider
    {
        public int GetSchedulerInterval() => configuration.SchedulerConfiguration.ScheduleIntervalSeconds;
    }
}
