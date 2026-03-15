using MediatR;
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
        [HttpPost("register")]
		public async Task<IActionResult> Register([FromBody] RegisterUserInput input, CancellationToken cancellationToken)
		{
			var request = new RegisterUserRequest(input);
			var result = await mediator.Send(request, cancellationToken);
			return AsResult(result);
		}

		[HttpPost("login")]
		public async Task<IActionResult> Login(CancellationToken cancellationToken)
		{
			return Ok();
		}

		[HttpPost("forgot-password")]
		public async Task<IActionResult> ForgatPassword(CancellationToken cancellationToken)
		{
			return Ok();
		}
	}
}
