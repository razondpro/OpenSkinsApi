using OpenSkinsApi.Infrastructure.Http.Middlewares;
using OpenSkinsApi.Infrastructure.Http.Api;
using Serilog;

namespace OpenSkinsApi.Infrastructure.Http
{
    public class Server
    {
        private readonly WebApplication _app;

        public Server(WebApplicationBuilder app)
        {
            _app = app.Build();

            ConfigureMiddlewares();

            ConfigureRoutes();

            ConfigureSwagger();

            ConfigureGlobalErrorHandling();
        }

        private void ConfigureMiddlewares()
        {
            //request body validation middleware (we only accept valid json)
            _app.UseMiddleware<JsonValidationMiddleware>();

            if (_app.Environment.IsDevelopment())
            {
                _app.UseSerilogRequestLogging();
                _app.UseDeveloperExceptionPage();
                _app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            }

            _app.UseAuthentication();
            _app.UseAuthorization();
        }

        private void ConfigureGlobalErrorHandling()
        {
            _app.UseErrorHandlingMiddleware();
        }

        private void ConfigureSwagger()
        {
            if (_app.Environment.IsDevelopment())
            {
                _app.UseSwagger();
                _app.UseSwaggerUI(
                    options =>
                    {
                        var descriptions = _app.DescribeApiVersions();
                        // build a swagger endpoint for each discovered API version
                        foreach (var description in descriptions)
                        {
                            var url = $"/swagger/{description.GroupName}/swagger.json";
                            var name = description.GroupName.ToUpperInvariant();
                            options.SwaggerEndpoint(url, name);
                        }
                    });
            }
        }

        private void ConfigureRoutes()
        {
            Log.Information("Starting server");
            _app.NewVersionedApi()
                .BuildRoutes();
        }

        public async Task RunAsync()
        {
            Log.Information("Stopping server");
            await _app.RunAsync();
        }

    }
}