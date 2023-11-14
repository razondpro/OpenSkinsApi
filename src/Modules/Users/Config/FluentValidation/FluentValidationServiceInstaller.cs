using FluentValidation;
using OpenSkinsApi.Config;
using OpenSkinsApi.Modules.Users.Application.FindUserByUserName;
using OpenSkinsApi.Modules.Users.Application.UpdateUser;
namespace OpenSkinsApi.Modules.Users.Config.FluentValidation
{
    public class FluentValidationServiceInstaller : IServiceInstaller
    {
        public void Install(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IValidator<UpdateUserHttpRequestDto>, UpdateUserHttpRequestValidator>();
            services.AddScoped<IValidator<FindUserByUserNameHttpRequestDto>, FindUserByUserNameHttpRequestValidator>();
        }

    }
}