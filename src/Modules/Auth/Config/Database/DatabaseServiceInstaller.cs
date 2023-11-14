using OpenSkinsApi.Config;
using OpenSkinsApi.Infrastructure.Persistence.Core.UnitOfWork;
using OpenSkinsApi.Modules.Auth.Application.Abstractions;
using OpenSkinsApi.Modules.Auth.Domain.Repositories;
using OpenSkinsApi.Modules.Auth.Infrastructure.Persistence;
using OpenSkinsApi.Modules.Auth.Infrastructure.Persistence.Core;
using OpenSkinsApi.Modules.Auth.Infrastructure.Persistence.Repositories.Implementations;

namespace OpenSkinsApi.Modules.Auth.Config.Database
{
    public class DatabaseServiceInstaller : IServiceInstaller
    {
        public void Install(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IAuthUnitOfWork, AuthUnitOfWork>();

            services.AddScoped<IAuthUserReadRepository, AuthUserReadRepository>();
            services.AddScoped<IAuthUserWriteRepository, AuthUserWriteRepository>();
            services.AddScoped<IAuthRefreshTokenReadRepository, AuthRefreshTokenReadRepository>();
        }
    }
}