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
}
