using OpenSkinsApi.Config;
using OpenSkinsApi.Infrastructure.Persistence.Core.UnitOfWork;
using OpenSkinsApi.Modules.Users.Application.Abstractions;
using OpenSkinsApi.Modules.Users.Domain.Repositories;
using OpenSkinsApi.Modules.Users.Infrastructure.Persistence;
using OpenSkinsApi.Modules.Users.Infrastructure.Persistence.Core;
using OpenSkinsApi.Modules.Users.Infrastructure.Persistence.Repositories.Implementations;

namespace OpenSkinsApi.Modules.Users.Config.Database
{
    public class DatabaseServiceInstaller : IServiceInstaller
    {
        public void Install(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUserUnitOfWork, UserUnitOfWork>();

            services.AddScoped<IUserReadRepository, UserReadRepository>();
            services.AddScoped<IUserWriteRepository, UserWriteRepository>();
        }
    }
}