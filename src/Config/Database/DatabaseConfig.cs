namespace OpenSkinsApi.Config.Database
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;
    using OpenSkinsApi.Infrastructure.Persistence;

    public static class DatabaseConfig
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services)
        {
            services.ConfigureOptions<DatabaseOptionsSetup>();
            services.AddDbContext<Database>((provider, options) =>
            {
                var databaseOptions = provider.GetRequiredService<IOptions<DatabaseOptions>>().Value;

                options.UseMySql(
                    databaseOptions.ConnectionString,
                    ServerVersion.AutoDetect(databaseOptions.ConnectionString),
                    mysqlOptions =>
                    {
                        mysqlOptions.EnableRetryOnFailure(databaseOptions.MaxRetryCount);
                        mysqlOptions.CommandTimeout(databaseOptions.CommandTimeout);
                    }
                );

                options.EnableDetailedErrors(databaseOptions.EnableDetailedErrors);
                options.EnableSensitiveDataLogging(databaseOptions.EnableSensitiveDataLogging);

            });

            return services;
        }
    }
}