namespace OpenSkinsApi.Infrastructure.Http.Api
{
    using Asp.Versioning.Builder;
    using OpenSkinsApi.Modules.Skins.Infrastructure.Http.Routes;

    public static class ApiBuilder
    {
        public const int V1 = 1;
        public static IVersionedEndpointRouteBuilder BuildRoutes(this IVersionedEndpointRouteBuilder application)
        {
            var apiV1 = application.MapGroup("/api/").HasApiVersion(V1);

            //skins routes v1
            var skinsV1 = apiV1.MapGroup("/skins/");
            SkinsRouteExtensions.MapSkinsRoutes(skinsV1).WithTags("Skins");

            return application;
        }
    }
}