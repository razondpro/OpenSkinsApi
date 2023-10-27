using OpenSkinsApi.Modules.Skins.Application.PurchaseSkin;
using OpenSkinsApi.Modules.Skins.Application.ChangePurchasedColor;
using OpenSkinsApi.Modules.Skins.Application.DeletePurchase;
using OpenSkinsApi.Modules.Skins.Application.FindAvailableSkins;
using OpenSkinsApi.Modules.Skins.Application.FindSkinById;
using OpenSkinsApi.Modules.Skins.Application.FindMySkins;

namespace OpenSkinsApi.Config.Http
{
    public class HttpServiceInstaller : IServiceInstaller
    {
        public void Install(IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpContextAccessor();
            services.AddCors();
            services.AddScoped<FindAvailableSkinsController>();
            services.AddScoped<FindSkinByIdController>();
            services.AddScoped<PurchaseSkinController>();
            services.AddScoped<DeletePurchaseController>();
            services.AddScoped<ChangePurchasedColorController>();
            services.AddScoped<FindMySkinsController>();
        }
    }
}