using TopKnow.Common.Configurations;

namespace TopKnow.Management.Api.Extensions
{
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddSecurityHeaders(this IServiceCollection services, IConfiguration configuration)
		{
			services.Configure<SecuritySettings>(configuration.GetSection(nameof(SecuritySettings)));
			return services;
		}

		public static IServiceCollection AddJwt(this IServiceCollection services, IConfiguration configuration)
		{
			services.Configure<AuthenticationSettings>(configuration.GetSection(nameof(AuthenticationSettings)));

			//TODO: Token'ın geçerli olabilmesi için işlemler gerekli
			return services;
		}
	}
}
