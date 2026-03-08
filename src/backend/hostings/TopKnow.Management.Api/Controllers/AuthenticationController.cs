using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using TopKnow.Common.Configurations;
using TopKnow.Modules.Management.Queries.Authentication;

namespace TopKnow.Management.Api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthenticationController : TopKnowController
{
    private readonly IMediator mediator;
    private readonly AuthenticationSettings settings;

    public AuthenticationController(IMediator mediator, IOptions<AuthenticationSettings> options)
    {
        this.mediator = mediator;
        this.settings = options.Value;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestInput input, CancellationToken cancellationToken)
    {
        var request = new LoginRequest(input, settings);
        var result = await mediator.Send(request, cancellationToken);
        return AsResult(result);
    }
}
