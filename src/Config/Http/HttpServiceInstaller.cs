
using OpenSkinsApi.Modules.Skins.Application.FindAvailableSkins;

namespace OpenSkinsApi.Config.Http
{
    public class HttpServiceInstaller : IServiceInstaller
    {
        public void Install(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<FindAvailableSkinsController>();
        }
    }
}