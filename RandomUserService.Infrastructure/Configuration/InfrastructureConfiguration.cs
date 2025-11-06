namespace RandomUserService.Infrastructure.Configuration
{
    public class InfrastructureConfiguration
    {
        public SchedulerConfiguration SchedulerConfiguration { get; set; }
    }

    public class SchedulerConfiguration
    {
        public int ScheduleIntervalSeconds { get; set; }
    }
}
