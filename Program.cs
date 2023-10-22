using OpenSkinsApi.Infrastructure.Http;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);


Server server = new(builder);
await server.RunAsync();