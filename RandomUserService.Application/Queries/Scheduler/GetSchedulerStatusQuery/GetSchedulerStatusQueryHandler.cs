using MediatR;
using RandomUserService.Infrastructure.Schedulers.Interfaces;

namespace RandomUserService.Application.Queries.Scheduler.GetSchedulerStatusQuery
{
    public class GetSchedulerStatusQueryHandler(IRandomUserServiceScheduler scheduler) : IRequestHandler<GetSchedulerStatusQuery, string>
    {
        public Task<string> Handle(GetSchedulerStatusQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(scheduler.GetStatus());
        }
    }
}
