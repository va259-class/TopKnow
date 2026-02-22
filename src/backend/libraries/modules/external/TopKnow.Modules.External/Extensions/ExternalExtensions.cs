using Microsoft.Extensions.DependencyInjection;
using TopKnow.Modules.External.Commands;
using TopKnow.Modules.External.Queries;

namespace TopKnow.Modules.External.Extensions
{
    public static class ExternalExtensions
    {
        public static IServiceCollection AddExternal(this IServiceCollection services)
        {
            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(ExternalCommandsAssemblyIndicator.GetAssembly());
                config.RegisterServicesFromAssembly(ExternalQueriesAssemblyIndicator.GetAssembly());
            });
            return services;
        }
    }
}
