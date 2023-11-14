using OpenSkinsApi.Config;
using OpenSkinsApi.Modules.Auth.Application.LoginUser;
using OpenSkinsApi.Modules.Auth.Application.RefreshToken;
using OpenSkinsApi.Modules.Auth.Application.RegisterUser;
using OpenSkinsApi.Modules.Auth.Application.RevokeToken;

namespace OpenSkinsApi.Modules.Skins.Config.Http
{
    public class HttpControllerServiceInstaller : IServiceInstaller
    {
        public void Install(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<RegisterUserHttpController>();
            services.AddScoped<LoginUserHttpController>();
            services.AddScoped<RefreshTokenHttpController>();
            services.AddScoped<RevokeTokenHttpController>();
        }
    }
}