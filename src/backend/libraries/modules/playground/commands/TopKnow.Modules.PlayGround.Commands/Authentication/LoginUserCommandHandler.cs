using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TopKnow.Common.Configurations;
using TopKnow.Common.Enums;
using TopKnow.Data.Context;
using TopKnow.Modules.Common.Helpers;

namespace TopKnow.Modules.PlayGround.Commands.Authentication;

public record LoginUserInput(string Mail, string Password);

public record LoginUserOutput(Guid Id, string DisplayName, UserType UserType, string Token);

public class LoginUserRequest : IRequest<Result<LoginUserOutput>>
{
	public LoginUserRequest(LoginUserInput input)
	{
		Input = input;
	}

	public LoginUserInput Input { get; }
}

internal class LoginUserCommandHandler : IRequestHandler<LoginUserRequest, Result<LoginUserOutput>>
{
	private readonly TopKnowContext context;
	private readonly AuthenticationSettings authSettings;

	public LoginUserCommandHandler(TopKnowContext context, IOptions<AuthenticationSettings> authSettings)
	{
		this.context = context;
		this.authSettings = authSettings.Value;
	}

	public async Task<Result<LoginUserOutput>> Handle(LoginUserRequest request, CancellationToken cancellationToken)
	{
		var user = await context.Users.FirstOrDefaultAsync(
			f => !f.IsDeleted &&
				 f.Mail == request.Input.Mail &&
				 f.Type == UserType.User,
			cancellationToken);

		if (user is null)
		{
			return Result<LoginUserOutput>.Failure(new Error(ErrorCodes.NOT_FOUND, request.Input.Mail));
		}

		var hasher = new PasswordHasher<object>();
		var verifyResult = hasher.VerifyHashedPassword(user.Id, user.Password, request.Input.Password);

		if (verifyResult != PasswordVerificationResult.Success)
		{
			return Result<LoginUserOutput>.Failure(new Error(ErrorCodes.INVALID_PARAMETER, request.Input.Mail));
		}

		var claims = new List<Claim>
		{
			new(ClaimTypes.NameIdentifier, user.Id.ToString()),
			new(ClaimTypes.Name, user.DisplayName),
			new(ClaimTypes.Role, user.Type.GetHashCode().ToString()),
		};

		var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authSettings.Key));
		var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
		var tokenDescriptor = new SecurityTokenDescriptor
		{
			Subject = new ClaimsIdentity(claims),
			Expires = DateTime.UtcNow.AddMinutes(authSettings.ExpiresInMinutes),
			SigningCredentials = credentials,
		};

		var handler = new JwtSecurityTokenHandler();
		var token = handler.CreateToken(tokenDescriptor);
		var jwtToken = handler.WriteToken(token);
		var output = new LoginUserOutput(user.Id, user.DisplayName, user.Type, jwtToken);
		return Result<LoginUserOutput>.Success(output);
	}
}
