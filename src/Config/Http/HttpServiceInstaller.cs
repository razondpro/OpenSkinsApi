using OpenSkinsApi.Modules.Skins.Application.BuySkin;
using OpenSkinsApi.Modules.Skins.Application.FindAvailableSkins;
using OpenSkinsApi.Modules.Skins.Application.FindSkinById;

namespace OpenSkinsApi.Config.Http
{
    public class HttpServiceInstaller : IServiceInstaller
    {
        public void Install(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<FindAvailableSkinsController>();
            services.AddScoped<FindSkinByIdController>();
            services.AddScoped<BuySkinController>();
        }
    }
}