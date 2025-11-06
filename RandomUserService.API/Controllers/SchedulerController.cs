using MediatR;
using Microsoft.AspNetCore.Mvc;
using RandomUserService.Application.Commands.Scheduler.PauseSchedulerCommand;
using RandomUserService.Application.Commands.Scheduler.ResumeSchedulerCommand;
using RandomUserService.Application.Queries.Scheduler.GetSchedulerStatusQuery;
using System.Threading.Tasks;

namespace RandomUserService.API.Controllers
{
    [ApiController]
    [Route("RandomUserService/api/scheduler")]
    public class SchedulerController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<string>> GetStatus()
        {
            return Ok(await mediator.Send(new GetSchedulerStatusQuery()));
        }

        [HttpPost]
        [Route("pause")]
        public async Task<ActionResult> Pause()
        {
            await mediator.Send(new PauseSchedulerCommand());

            return NoContent();
        }

        [HttpPost]
        [Route("resume")]
        public async Task<ActionResult> Resume()
        {
            await mediator.Send(new ResumeSchedulerCommand());

            return NoContent();
        }
    }
}
