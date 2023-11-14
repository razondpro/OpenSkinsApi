namespace OpenSkinsApi.Config.Database
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;
    using OpenSkinsApi.Infrastructure.Persistence;
    using OpenSkinsApi.Infrastructure.Persistence.Core.Interceptors;
    using OpenSkinsApi.Modules.Auth.Infrastructure.Persistence;
    using OpenSkinsApi.Modules.Skins.Infrastructure.Persistence;
    using OpenSkinsApi.Modules.Users.Infrastructure.Persistence;
    public class DatabaseServiceInstaller : IServiceInstaller
    {
        public void Install(IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureOptions<DatabaseOptionsSetup>();

            AddDbContext<AuthDatabase>(services);
            AddDbContext<SkinDatabase>(services);
            AddDbContext<UserDatabase>(services);
            AddDbContext<Database>(services);

            services.AddSingleton<UpdateAuditableEntitiesInterceptor>();

            // Create a new service scope to perform the database migration
            using var scope = services.BuildServiceProvider().CreateScope();
            var serviceProvider = scope.ServiceProvider;
            var dbContext = serviceProvider.GetRequiredService<Database>();

            if (dbContext.Database.GetPendingMigrations().Any())
            {
                dbContext.Database.Migrate();
            }
        }

        private static void AddDbContext<TContext>(IServiceCollection services) where TContext : Database
        {
            services.AddDbContext<TContext>((provider, options) =>
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

                var updateAuditableEntitiesInterceptor = provider.GetRequiredService<UpdateAuditableEntitiesInterceptor>();
                options.AddInterceptors(updateAuditableEntitiesInterceptor);
            });
        }
    }
}