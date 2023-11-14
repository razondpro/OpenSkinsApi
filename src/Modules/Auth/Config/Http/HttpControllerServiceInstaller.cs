using OpenSkinsApi.Modules.Skins.Application.PurchaseSkin;
using OpenSkinsApi.Modules.Skins.Application.ChangePurchasedColor;
using OpenSkinsApi.Modules.Skins.Application.DeletePurchase;
using OpenSkinsApi.Modules.Skins.Application.FindAvailableSkins;
using OpenSkinsApi.Modules.Skins.Application.FindSkinById;
using OpenSkinsApi.Modules.Skins.Application.FindMySkins;
using OpenSkinsApi.Config;

namespace OpenSkinsApi.Modules.Auth.Config.Http
{
    public class HttpControllerServiceInstaller : IServiceInstaller
    {
        public void Install(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<FindAvailableSkinsController>();
            services.AddScoped<FindSkinByIdController>();
            services.AddScoped<PurchaseSkinController>();
            services.AddScoped<DeletePurchaseController>();
            services.AddScoped<ChangePurchasedColorController>();
            services.AddScoped<FindMySkinsController>();
        }
    }
}