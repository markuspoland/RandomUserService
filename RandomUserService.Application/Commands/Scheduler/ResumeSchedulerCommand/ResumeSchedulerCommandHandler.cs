using MediatR;
using RandomUserService.Infrastructure.Schedulers.Interfaces;

namespace RandomUserService.Application.Commands.Scheduler.ResumeSchedulerCommand
{
    public class ResumeSchedulerCommandHandler(IRandomUserServiceScheduler scheduler) : IRequestHandler<ResumeSchedulerCommand>
    {
        public Task Handle(ResumeSchedulerCommand request, CancellationToken cancellationToken)
        {
            scheduler.Resume();

            return Task.CompletedTask;
        }
    }
}
