using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TopKnow.Common.Configurations;
using TopKnow.Common.Enums;
using TopKnow.Data.Context;
using TopKnow.Modules.Common.Helpers;

namespace TopKnow.Modules.Management.Queries.Authentication;
public record LoginRequestOutput(Guid Id, string DisplayName, UserType UserType, string Token);
public class LoginRequestInput
{
    public string Mail { get; set; }
    public string Password { get; set; }
}

public class LoginRequest : IRequest<Result<LoginRequestOutput>>
{
    public LoginRequest(LoginRequestInput input, AuthenticationSettings settings)
    {
        Input = input;
        Settings = settings;
    }

    public LoginRequestInput Input { get; }
    public AuthenticationSettings Settings { get; }
}

internal class LoginRequestHandler : IRequestHandler<LoginRequest, Result<LoginRequestOutput>>
{
    private readonly TopKnowContext context;

    public LoginRequestHandler(TopKnowContext context)
    {
        this.context = context;
    }
    public async Task<Result<LoginRequestOutput>> Handle(LoginRequest request, CancellationToken cancellationToken)
    {
        var user = await context.Users.FirstOrDefaultAsync(f => !f.IsDeleted &&
                                                                f.Mail == request.Input.Mail &&
                                                                f.Type == UserType.Admin);
        if (user is null)
        {
            return Result<LoginRequestOutput>.Failure(new Error(ErrorCodes.NOT_FOUND, request.Input.Mail));
        }

        var hasher = new PasswordHasher<object>();
        var verifyResult = hasher.VerifyHashedPassword(user.Id, user.Password, request.Input.Password);

        if (verifyResult != PasswordVerificationResult.Success)
        {
            return Result<LoginRequestOutput>.Failure(new Error(ErrorCodes.INVALID_PARAMETER, request.Input.Mail));
        }

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.DisplayName),
            new Claim(ClaimTypes.Role, user.Type.GetHashCode().ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(request.Settings.Key));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(request.Settings.ExpiresInMinutes),
            SigningCredentials = credentials
        };

        // Burada bir de refresh token olursa kullanıcının sürekli authorized olmasına destek oluruz
        var handler = new JwtSecurityTokenHandler();
        var token = handler.CreateToken(tokenDescriptor);
        var jwtToken = handler.WriteToken(token);
        var result = new LoginRequestOutput(user.Id, user.DisplayName, user.Type, jwtToken);
        return Result<LoginRequestOutput>.Success(result);
    }
}
