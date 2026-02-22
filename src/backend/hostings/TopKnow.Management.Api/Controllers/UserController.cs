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
    public IActionResult GetAdminUsers([FromQuery] PagedQueryParameter parameter)
    {
        var request = new GetPagedAdminUsersRequest(parameter);
        var result = mediator.Send(request, CancellationToken.None);
        return Ok(result);
    }

    [HttpGet("players")]
    public IActionResult GetPlayerUsers([FromQuery] PagedUserRequestInput parameter)
    {
        var result = mediator.Send(null, CancellationToken.None);
        return Ok(result);
    }

    [HttpGet("external-users")]
    public IActionResult GetApiUsers([FromQuery] PagedQueryParameter parameter)
    {
        var result = mediator.Send(null, CancellationToken.None);
        return Ok(result);
    }
}
