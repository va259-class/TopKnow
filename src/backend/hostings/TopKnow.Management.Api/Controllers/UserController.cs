using MediatR;
using Microsoft.AspNetCore.Mvc;
using TopKnow.Common.Concretes;
using TopKnow.Modules.Management.Queries.Users;
using TopKnow.Modules.Management.RequestInputs;

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
    public IActionResult GetPlayerUsers([FromQuery] PagedUserRequestInput parameter, CancellationToken cancellationToken)
    {
        var result = mediator.Send(null, cancellationToken);
        return Ok(result);
    }

    [HttpGet("external-users")]
    public IActionResult GetApiUsers([FromQuery] PagedQueryParameter parameter, CancellationToken cancellationToken)
    {
        var result = mediator.Send(null, cancellationToken);
        return Ok(result);
    }
}
