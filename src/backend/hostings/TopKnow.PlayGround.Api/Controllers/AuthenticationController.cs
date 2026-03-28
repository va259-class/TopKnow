using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TopKnow.Modules.PlayGround.Commands.Authentication;

namespace TopKnow.PlayGround.Api.Controllers
{
	[ApiController]
	[Route("api/authentication")]
	public class AuthenticationController : TopKnowController
	{
		private readonly IMediator mediator;

		public AuthenticationController(IMediator mediator)
        {
			this.mediator = mediator;
		}

		[AllowAnonymous]
        [HttpPost("register")]
		public async Task<IActionResult> Register([FromBody] RegisterUserInput input, CancellationToken cancellationToken)
		{
			var request = new RegisterUserRequest(input);
			var result = await mediator.Send(request, cancellationToken);
			return AsResult(result);
		}

		[AllowAnonymous]
		[HttpPost("login")]
		public async Task<IActionResult> Login([FromBody] LoginUserInput input, CancellationToken cancellationToken)
		{
			var result = await mediator.Send(new LoginUserRequest(input), cancellationToken);
			return AsResult(result);
		}

		[AllowAnonymous]
		[HttpPost("forgot-password")]
		public IActionResult ForgotPassword()
		{
			return Ok();
		}
	}
}
