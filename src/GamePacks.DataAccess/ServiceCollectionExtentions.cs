using GamePacks.Service;
using GamePacks.DataAccess;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace GamePacks.DataAccess;

public static class ServiceCollectionExtentions
{
    public static IServiceCollection ConfigureDataAccessServices(this IServiceCollection services, Action<GamePacksDatabaseOptions> configAction)
    {
        services.Configure(configAction);
        services.AddDbContext<GamePacksDbContext>((serviceProvider, options) =>
        {
            var dbSettings = serviceProvider.GetRequiredService<IOptions<GamePacksDatabaseOptions>>().Value;
            options.UseNpgsql(dbSettings.ConnectionString,b => b.MigrationsAssembly("GamePacks.Migrations"));
        });

        services.AddScoped<IPackRepository, PackRepository>();

        return services;
    }
}
