using OpenSkinsApi.Config;
using OpenSkinsApi.Infrastructure.Http;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Host.InstallHosts(builder.Configuration, typeof(Program).Assembly);
builder.Services.InstallServices(builder.Configuration, typeof(Program).Assembly);

Server server = new(builder);
await server.RunAsync();