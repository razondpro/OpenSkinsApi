using Microsoft.AspNetCore.Mvc;
using OpenSkinsApi.Modules.Skins.Application.FindAvailableSkins;

namespace OpenSkinsApi.Modules.Skins.Infrastructure.Http.Routes
{
    public static class SkinsRouteExtensions
    {
        public static RouteGroupBuilder MapSkinsRoutes(this RouteGroupBuilder builder)
        {
            builder.MapGet("/available", async (
                CancellationToken cancellationToken,
                FindAvailableSkinsController controller,
                [AsParameters] FindAvailableSkinsRequestDto dto) =>
            {
                return await controller.Execute(dto, cancellationToken);
            })
            .WithName("FindAvailableSkins")
            .WithDescription("Find available skins");


            return builder;
        }

    }
}