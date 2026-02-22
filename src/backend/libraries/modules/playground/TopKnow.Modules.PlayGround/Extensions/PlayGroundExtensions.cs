using Microsoft.Extensions.DependencyInjection;
using TopKnow.Modules.PlayGround.Commands;
using TopKnow.Modules.PlayGround.Queries;

namespace TopKnow.Modules.PlayGround.Extensions;

public static class PlayGroundExtensions
{
    public static IServiceCollection AddPlayGround(this IServiceCollection services)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(PlayGroundCommandsAssemblyIndicator.GetAssembly());
            config.RegisterServicesFromAssembly(PlayGroundQueriesAssemblyIndicator.GetAssembly());
        });
        return services;
    }
}
