using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using TopKnow.Common.Configurations;

namespace TopKnow.PlayGround.Api.Extensions;

public static class ServiceCollectionExtensions
{
	private const string SignalRHubPath = "/gh";

	public static IServiceCollection AddJwt(this IServiceCollection services, IConfiguration configuration)
	{
		services.Configure<AuthenticationSettings>(configuration.GetSection(nameof(AuthenticationSettings)));

		var authSettings = configuration.GetSection(nameof(AuthenticationSettings)).Get<AuthenticationSettings>()
			?? throw new InvalidOperationException("AuthenticationSettings is missing.");
		if (string.IsNullOrWhiteSpace(authSettings.Key))
		{
			throw new InvalidOperationException("AuthenticationSettings.Key is required.");
		}

		var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authSettings.Key));

		services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
			.AddJwtBearer(options =>
			{
				options.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuer = false,
					ValidateAudience = false,
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = signingKey,
					ClockSkew = TimeSpan.Zero,
				};
				options.Events = new JwtBearerEvents
				{
					OnMessageReceived = context =>
					{
						var accessToken = context.Request.Query["access_token"];
						var path = context.HttpContext.Request.Path;
						if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments(SignalRHubPath))
						{
							context.Token = accessToken;
						}

						return Task.CompletedTask;
					},
				};
			});

		services.AddAuthorization();
		return services;
	}
}
