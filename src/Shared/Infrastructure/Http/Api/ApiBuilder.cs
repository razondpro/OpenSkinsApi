namespace OpenSkinsApi.Infrastructure.Http.Api
{
    using Asp.Versioning.Builder;
    using OpenSkinsApi.Modules.Auth.Infrastructure.Http.Routes;
    using OpenSkinsApi.Modules.Skins.Infrastructure.Http.Routes;
    using OpenSkinsApi.Modules.Users.Infrastructure.Http.Routes;

    public static class ApiBuilder
    {
        public const int V1 = 1;
        public static IVersionedEndpointRouteBuilder BuildRoutes(this IVersionedEndpointRouteBuilder application)
        {
            var apiV1 = application.MapGroup("/api/").HasApiVersion(V1);

            //skins routes v1
            var skinsV1 = apiV1.MapGroup("/skins/");
            SkinsRouteExtensions.MapSkinsRoutes(skinsV1).WithTags("Skins");

            //auth routes v1
            var authV1 = apiV1.MapGroup("/auth/");
            AuthRouteExtensions.MapAuthRoutes(authV1).WithTags("Auth");

            //user routes v1
            var userV1 = apiV1.MapGroup("/users/");
            UserRouteExtensions.MapUserRoutes(userV1).WithTags("Users");

            return application;
        }
    }
}