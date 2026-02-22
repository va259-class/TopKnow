using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TopKnow.Common.Configurations;
using TopKnow.Data.Context;

namespace TopKnow.Data.Extensions;

public static class DataExtensions
{
    public static IServiceCollection AddData(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<DatabaseSettings>(configuration.GetSection(nameof(DatabaseSettings)));

        var settings = configuration.GetSection(nameof(DatabaseSettings)).Get<DatabaseSettings>();
        services.AddDbContext<TopKnowContext>(options =>
        {
            options.UseSqlServer(settings.Main);
        });
        return services;
    }
}
