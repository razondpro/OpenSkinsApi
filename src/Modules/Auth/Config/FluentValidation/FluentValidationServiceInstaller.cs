using FluentValidation;
using OpenSkinsApi.Modules.Auth.Application.RegisterUser;
using OpenSkinsApi.Modules.Auth.Application.LoginUser;
using OpenSkinsApi.Modules.Auth.Application.RefreshToken;
using OpenSkinsApi.Modules.Auth.Application.RevokeToken;
using OpenSkinsApi.Config;

namespace OpenSkinsApi.Modules.Auth.Config.FluentValidation
{
    public class FluentValidationServiceInstaller : IServiceInstaller
    {
        public void Install(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IValidator<RegisterUserRequestDto>, RegisterUserValidator>();
            services.AddScoped<IValidator<LoginUserRequestDto>, LoginUserValidator>();
            services.AddScoped<IValidator<RefreshTokenRequestDto>, RefreshTokenValidator>();
            services.AddScoped<IValidator<RevokeTokenRequestDto>, RevokeTokenValidator>();
        }
    }
}