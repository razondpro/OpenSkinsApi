using OpenSkinsApi.Modules.Skins.Application.PurchaseSkin;
using OpenSkinsApi.Modules.Skins.Application.ChangePurchasedColor;
using OpenSkinsApi.Modules.Skins.Application.DeletePurchase;
using OpenSkinsApi.Modules.Skins.Application.FindAvailableSkins;
using OpenSkinsApi.Modules.Skins.Application.FindSkinById;
using OpenSkinsApi.Modules.Skins.Application.FindMySkins;
using OpenSkinsApi.Modules.Auth.Application.RegisterUser;
using OpenSkinsApi.Modules.Auth.Application.LoginUser;
using OpenSkinsApi.Modules.Auth.Application.RefreshToken;
namespace OpenSkinsApi.Config.Http
{
    public class HttpServiceInstaller : IServiceInstaller
    {
        public void Install(IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpContextAccessor();
            services.AddCors();
        }
    }
}