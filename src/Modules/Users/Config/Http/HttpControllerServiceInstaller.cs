using OpenSkinsApi.Config;
using OpenSkinsApi.Modules.Users.Application.FindUserByUserName;
using OpenSkinsApi.Modules.Users.Application.UpdateUser;

namespace OpenSkinsApi.Modules.Users.Config.Http
{
    public class HttpControllerServiceInstaller : IServiceInstaller
    {
        public void Install(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<UpdateUserHttpController>();
            services.AddScoped<FindUserByUserNameHttpController>();

        }
    }
}