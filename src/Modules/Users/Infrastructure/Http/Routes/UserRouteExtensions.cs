namespace OpenSkinsApi.Modules.Users.Infrastructure.Http.Routes
{
    using Microsoft.AspNetCore.Mvc;
    using OpenSkinsApi.Modules.Users.Application.FindUserByUserName;
    using OpenSkinsApi.Modules.Users.Application.UpdateUser;
    using OpenSkinsApi.Infrastructure.Http.Core;
    using OpenSkinsApi.Infrastructure.Http.Filters;

    public static class UserRouteExtensions
    {
        public static RouteGroupBuilder MapUserRoutes(this RouteGroupBuilder builder)
        {

            builder.MapPut("/", async (
                CancellationToken cancellationToken,
                UpdateUserHttpController controller,
                [FromBody] UpdateUserHttpRequestDto req) =>
            {
                return await controller.Execute(req, cancellationToken);
            })
            .RequireAuthorization()
            .AddEndpointFilter<ValidationFilter<UpdateUserHttpRequestDto>>()
            .WithName("UpdateUser")
            .WithDescription("Update an user")
            .Produces<ApiHttpErrorResponse>(StatusCodes.Status400BadRequest);


            builder.MapGet("/{userName}", async (
                CancellationToken cancellationToken,
                FindUserByUserNameHttpController controller,
                [AsParameters] FindUserByUserNameHttpRequestDto userName) =>
            {
                return await controller.Execute(userName, cancellationToken);
            }) 
            .AddEndpointFilter<ValidationFilter<FindUserByUserNameHttpRequestDto>>()
            .WithName("FindUserByUserName")
            .WithDescription("Get user by userName")
            .Produces<ApiHttpErrorResponse>(StatusCodes.Status400BadRequest);

            return builder;
        }
    }
}
