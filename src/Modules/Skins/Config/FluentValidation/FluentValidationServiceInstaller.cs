
using FluentValidation;
using OpenSkinsApi.Modules.Skins.Application.PurchaseSkin;
using OpenSkinsApi.Modules.Skins.Application.ChangePurchasedColor;
using OpenSkinsApi.Modules.Skins.Application.DeletePurchase;
using OpenSkinsApi.Modules.Skins.Application.FindSkinById;
using OpenSkinsApi.Config;

namespace OpenSkinsApi.Modules.Skins.Config.FluentValidation
{
    public class FluentValidationServiceInstaller : IServiceInstaller
    {
        public void Install(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IValidator<FindSkinByIdRequestDto>, FindSkinByIdValidator>();
            services.AddScoped<IValidator<PurchaseSkinRequestDto>, PurchaseSkinRequestValidator>();
            services.AddScoped<IValidator<DeletePurchaseRequestDto>, DeletePurchaseRequestValidator>();
            services.AddScoped<IValidator<ChangePurchasedColorRequestDto>, ChangePurchasedColorRequestValidator>();
        }
    }
}