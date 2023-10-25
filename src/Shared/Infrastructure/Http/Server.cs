using OpenSkinsApi.Infrastructure.Http.Middlewares;
using OpenSkinsApi.Infrastructure.Http.Api;

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
                _app.UseDeveloperExceptionPage();
            }
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
            _app.NewVersionedApi()
                .BuildRoutes();
        }

        public async Task RunAsync()
        {
            await _app.RunAsync();
        }

    }
}