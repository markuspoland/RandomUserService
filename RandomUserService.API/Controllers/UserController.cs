using MediatR;
using Microsoft.AspNetCore.Mvc;
using RandomUserService.Application.DTO;
using RandomUserService.Application.Queries.User.GetUserQuery;
using RandomUserService.Application.Queries.User.GetUsersQuery;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RandomUserService.API.Controllers
{
    [ApiController]
    [Route("RandomUserService/api/user")]
    public class UserController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<IEnumerable<UserInfoDto>>> GetAll()
        {
            return Ok(await mediator.Send(new GetUsersQuery()));
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<DetailedUserInfoDto>> Get(Guid id)
        {
            return Ok(await mediator.Send(new GetUserQuery(id)));
        }
    }
}
