using OpenSkinsApi.Infrastructure.Persistence.Core.Interceptors;

namespace OpenSkinsApi.Config.Database
{
    public class DatabaseServiceInstaller : IServiceInstaller
    {
        public void Install(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDatabase();
            services.AddSingleton<UpdateAuditableEntitiesInterceptor>();
        }
    }
}