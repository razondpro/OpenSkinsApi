using Microsoft.AspNetCore.Mvc;
using OpenSkinsApi.Infrastructure.Http.Core;
using OpenSkinsApi.Infrastructure.Http.Filters;
using OpenSkinsApi.Modules.Skins.Application.PurchaseSkin;
using OpenSkinsApi.Modules.Skins.Application.ChangePurchasedColor;
using OpenSkinsApi.Modules.Skins.Application.DeletePurchase;
using OpenSkinsApi.Modules.Skins.Application.FindAvailableSkins;
using OpenSkinsApi.Modules.Skins.Application.FindSkinById;

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

            builder.MapGet("/{id}", async (
                CancellationToken cancellationToken,
                FindSkinByIdController controller,
                [AsParameters] FindSkinByIdRequestDto dto) =>
            {
                return await controller.Execute(dto, cancellationToken);
            })
            .AddEndpointFilter<ValidationFilter<FindSkinByIdRequestDto>>()
            .WithName("FindSkinById")
            .WithDescription("Find skin by Id")
            .Produces<ApiHttpErrorResponse>(StatusCodes.Status400BadRequest);

            builder.MapPost("/purchase", async (
                CancellationToken cancellationToken,
                PurchaseSkinController controller,
                PurchaseSkinRequestDto dto) =>
            {
                return await controller.Execute(dto, cancellationToken);
            })
            .AddEndpointFilter<ValidationFilter<PurchaseSkinRequestDto>>()
            .WithName("PurchaseSkin")
            .WithDescription("Purchase a new skin")
            .Produces<ApiHttpErrorResponse>(StatusCodes.Status400BadRequest);

            builder.MapDelete("/purchase/{purchaseId}", async (
                CancellationToken cancellationToken,
                DeletePurchaseController controller,
                [AsParameters] DeletePurchaseRequestDto dto) =>
            {
                return await controller.Execute(dto, cancellationToken);
            })
            .AddEndpointFilter<ValidationFilter<DeletePurchaseRequestDto>>()
            .WithName("DeletePurchase")
            .WithDescription("Delete purchase")
            .Produces<ApiHttpErrorResponse>(StatusCodes.Status400BadRequest);

            builder.MapPut("/purchase/color", async (
                CancellationToken cancellationToken,
                ChangePurchasedColorController controller,
                [FromBody] ChangePurchasedColorRequestDto dto) =>
            {
                return await controller.Execute(dto, cancellationToken);
            })
            .AddEndpointFilter<ValidationFilter<ChangePurchasedColorRequestDto>>()
            .WithName("ChangePurchasedColor")
            .WithDescription("Change aquired skin color")
            .Produces<ApiHttpErrorResponse>(StatusCodes.Status400BadRequest);


            return builder;
        }

    }
}