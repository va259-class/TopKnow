using MediatR;
using Microsoft.AspNetCore.Mvc;
using TopKnow.Common.Concretes;
using TopKnow.Modules.Management.Queries.Users;

namespace TopKnow.Management.Api.Controllers;

[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly IMediator mediator;

    public UserController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpGet("admins")]
    public async Task<IActionResult> GetAdminUsers([FromQuery] PagedQueryParameter parameter, CancellationToken cancellationToken)
    {
        var request = new GetPagedAdminUsersRequest(parameter);
        var result = await mediator.Send(request, cancellationToken);
        if (result.IsSuccess)
        {
            return Ok(result);
        }
        return BadRequest(result.Error);
    }

    [HttpGet("players")]
    public async Task<IActionResult> GetPlayerUsers([FromQuery] PagedUserRequestInput parameter, CancellationToken cancellationToken)
    {
        var request = new GetPagedPlayersRequest(parameter);
        var result = await mediator.Send(request, cancellationToken);
        if (result.IsSuccess)
        {
            return Ok(result);
        }
        return BadRequest(result.Error);
    }

    [HttpGet("external-users")]
    public IActionResult GetApiUsers([FromQuery] PagedQueryParameter parameter, CancellationToken cancellationToken)
    {
        var result = mediator.Send(null, cancellationToken);
        return Ok(result);
    }
}
