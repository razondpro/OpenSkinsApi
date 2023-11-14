using OpenSkinsApi.Config;
using OpenSkinsApi.Modules.Skins.Application.Abstractions;
using OpenSkinsApi.Modules.Skins.Domain.Repositories;
using OpenSkinsApi.Modules.Skins.Infrastructure.Persistence.Core;
using OpenSkinsApi.Modules.Skins.Infrastructure.Persistence.Repositories.Implementations;

namespace OpenSkinsApi.Modules.Skins.Config.Database
{
    public class DatabaseServiceInstaller : IServiceInstaller
    {
        public void Install(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ISkinUnitOfWork, SkinUnitOfWork>();

            services.AddScoped<ISkinReadRepository, SkinReadRepository>();
            services.AddScoped<ISkinWriteRepository, SkinWriteRepository>();
            services.AddScoped<IOwnerReadRepository, OwnerReadRepository>();
            services.AddScoped<IPurchaseReadRepository, PurchaseReadRepository>();
        }
    }
}