namespace OpenSkinsApi.Config.Swagger
{
    using Microsoft.Extensions.Options;
    using OpenSkinsApi.Config;
    using Swashbuckle.AspNetCore.SwaggerGen;
    public class SwaggerServiceInstaller : IServiceInstaller
    {
        public void Install(IServiceCollection services, IConfiguration configuration)
        {
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            services.AddSwaggerGen(options => options.OperationFilter<SwaggerDefaultValues>());
        }
    }

}