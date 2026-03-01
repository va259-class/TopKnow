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
	}
}
