namespace OpenSkinsApi.Modules.Auth.Config.Authentication
{
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using OpenSkinsApi.Config;
    using OpenSkinsApi.Modules.Auth.Application.Abstractions;
    using OpenSkinsApi.Modules.Auth.Infrastructure.Http.Jwt;
    using Serilog;

    public class JwtServiceInstaller : IServiceInstaller
    {
        public void Install(IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureOptions<JwtOptionsSetup>();
            services.ConfigureOptions<JwtBearerOptionsSetup>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();
            services.AddAuthorization();

            services.AddScoped<IJwtProvider, JwtProvider>();
        }
    }

}