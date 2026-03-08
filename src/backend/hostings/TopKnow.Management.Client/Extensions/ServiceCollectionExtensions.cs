using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using TopKnow.Common.Configurations;
using TopKnow.Management.Client.HttpClients;

namespace TopKnow.Management.Client.Extensions;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddManagementApi(this IServiceCollection services, IConfiguration configuration)
	{
		services.Configure<ExternalApiSettings>(configuration.GetSection(nameof(ExternalApiSettings)));
		services.Configure<SecuritySettings>(configuration.GetSection(nameof(SecuritySettings)));

		var externalApi = configuration.GetSection(nameof(ExternalApiSettings)).Get<ExternalApiSettings>();
		var securitySettings = configuration.GetSection(nameof(SecuritySettings)).Get<SecuritySettings>();

		services.AddHttpClient<ManagementApi>(nameof(ManagementApi), client =>
		{
			client.BaseAddress = new Uri(externalApi.ManagementApi);
			client.DefaultRequestHeaders.Add("top-know-security-header", securitySettings.HeaderKey);
		});
		return services;
	}

	public static IServiceCollection AddCookieAuthentication(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddAuthentication(options =>
		{
            options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        })
        .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
        {
            options.LoginPath = "/Authentication/Login";
            options.LogoutPath = "/Authentication/Logout";
            options.AccessDeniedPath = "/Authentication/AccessDenied";

            options.Cookie.HttpOnly = true;
            options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            options.ExpireTimeSpan = TimeSpan.FromHours(1);
            options.SlidingExpiration = true;
        });
		return services;
	}
}
