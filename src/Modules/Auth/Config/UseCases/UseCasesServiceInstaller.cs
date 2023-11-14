using OpenSkinsApi.Application.Core;
using OpenSkinsApi.Config;
using OpenSkinsApi.Modules.Auth.Application.LoginUser;
using OpenSkinsApi.Modules.Auth.Application.RefreshToken;

namespace OpenSkinsApi.Modules.Auth.Config.UseCases
{
    public class UseCasesServiceInstaller : IServiceInstaller
    {
        public void Install(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUseCase<LoginUserRequestDto, LoginUserUseCaseResult>, LoginUserUseCase>();
            services.AddScoped<IUseCase<RefreshTokenRequestDto, RefreshTokenUseCaseResult>, RefreshTokenUseCase>();
        }
    }
}