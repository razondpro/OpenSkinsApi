using OpenSkinsApi.Infrastructure.Persistence.Core.Interceptors;
using OpenSkinsApi.Infrastructure.Persistence.Core.UnitOfWork;

namespace OpenSkinsApi.Config.Database
{
    public class DatabaseServiceInstaller : IServiceInstaller
    {
        public void Install(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDatabase();
            services.AddSingleton<UpdateAuditableEntitiesInterceptor>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}