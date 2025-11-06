using MediatR;
using RandomUserService.Infrastructure.Schedulers.Interfaces;

namespace RandomUserService.Application.Commands.Scheduler.PauseSchedulerCommand
{
    public class PauseSchedulerCommandHandler(IRandomUserServiceScheduler scheduler) : IRequestHandler<PauseSchedulerCommand>
    {
        public Task Handle(PauseSchedulerCommand request, CancellationToken cancellationToken)
        {
            scheduler.Pause();

            return Task.CompletedTask;
        }
    }
}
