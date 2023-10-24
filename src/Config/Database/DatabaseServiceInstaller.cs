using OpenSkinsApi.Infrastructure.Persistence.Core.Interceptors;
using OpenSkinsApi.Infrastructure.Persistence.Core.UnitOfWork;
using OpenSkinsApi.Modules.Skins.Domain.Repositories;
using OpenSkinsApi.Modules.Skins.Infrastructure.Persistence.Repositories.Implementations;

namespace OpenSkinsApi.Config.Database
{
    public class DatabaseServiceInstaller : IServiceInstaller
    {
        public void Install(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDatabase();
            services.AddSingleton<UpdateAuditableEntitiesInterceptor>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ISkinReadRepository, SkinReadRepository>();
            services.AddScoped<ISkinWriteRepository, SkinWriteRepository>();
        }
    }
}