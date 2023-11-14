namespace OpenSkinsApi.Modules.Auth.Infrastructure.Http.Routes
{
    using OpenSkinsApi.Modules.Auth.Application.LoginUser;
    using OpenSkinsApi.Modules.Auth.Application.RefreshToken;
    using OpenSkinsApi.Modules.Auth.Application.RegisterUser;
    using OpenSkinsApi.Modules.Auth.Application.RevokeToken;
    using OpenSkinsApi.Infrastructure.Http.Core;
    using OpenSkinsApi.Infrastructure.Http.Filters;

    public static class AuthRouteExtensions
    {
        public static RouteGroupBuilder MapAuthRoutes(this RouteGroupBuilder builder)
        {

            builder.MapPost("/register", async (
                CancellationToken cancellationToken,
                RegisterUserHttpController controller,
                RegisterUserRequestDto req) =>
            {
                return await controller.Execute(req, cancellationToken);
            })
            .AddEndpointFilter<ValidationFilter<RegisterUserRequestDto>>()
            .WithName("RegisterUser")
            .WithDescription("Create a new user")
            .Produces<ApiHttpErrorResponse>(StatusCodes.Status400BadRequest);

            builder.MapPost("/login", async (
                CancellationToken cancellationToken,
                LoginUserHttpController controller,
                LoginUserRequestDto req) =>
            {
                return await controller.Execute(req, cancellationToken);
            })
            .AddEndpointFilter<ValidationFilter<LoginUserRequestDto>>()
            .WithName("LoginUser")
            .WithDescription("Log in a user")
            .Produces<ApiHttpErrorResponse>(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden);

            builder.MapPost("/refresh", async (
                CancellationToken cancellationToken,
                RefreshTokenHttpController controller,
                RefreshTokenRequestDto req) =>
            {
                return await controller.Execute(req, cancellationToken);
            })
            .AddEndpointFilter<ValidationFilter<RefreshTokenRequestDto>>()
            .WithName("RefreshToken")
            .WithDescription("Refresh a user's token")
            .Produces<ApiHttpErrorResponse>(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized);

            builder.MapPost("/revoke", async (
                CancellationToken cancellationToken,
                RevokeTokenHttpController controller,
                RevokeTokenRequestDto req) =>
            {
                return await controller.Execute(req, cancellationToken);
            })
            .AddEndpointFilter<ValidationFilter<RevokeTokenRequestDto>>()
            .WithName("RevokeToken")
            .WithDescription("Revoke a user's token")
            .Produces<ApiHttpErrorResponse>(StatusCodes.Status400BadRequest);


            return builder;
        }
    }
}
