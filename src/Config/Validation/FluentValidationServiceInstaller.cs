
using FluentValidation;
using OpenSkinsApi.Modules.Skins.Application.BuySkin;
using OpenSkinsApi.Modules.Skins.Application.FindSkinById;

namespace OpenSkinsApi.Config.Validation
{
    public class FluentValidationServiceInstaller : IServiceInstaller
    {
        public void Install(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IValidator<FindSkinByIdRequestDto>, FindSkinByIdValidator>();
            services.AddScoped<IValidator<BuySkinRequestDto>, BuySkinRequestValidator>();
        }
    }
}