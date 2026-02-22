using Microsoft.Extensions.DependencyInjection;
using TopKnow.Modules.Management.Commands;
using TopKnow.Modules.Management.Queries;

namespace TopKnow.Modules.Management.Extensions;

public static class ManagementExtensions
{
    public static IServiceCollection AddManagement(this IServiceCollection services)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(ManagementCommandsAssemblyIndicator.GetAssembly());
            config.RegisterServicesFromAssembly(ManagementQueriesAssemblyIndicator.GetAssembly());
        });
        return services;
    }
}
