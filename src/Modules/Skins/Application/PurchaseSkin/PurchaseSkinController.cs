using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using OpenSkinsApi.Application.Core;
using OpenSkinsApi.Infrastructure.Http.Core;

namespace OpenSkinsApi.Modules.Skins.Application.PurchaseSkin
{
    public class PurchaseSkinController :
        IHttpController<PurchaseSkinRequestDto, Results<NoContent, BadRequest<ApiHttpErrorResponse>, StatusCodeHttpResult>>
    {
        private readonly IMediator _mediator;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public PurchaseSkinController(IMediator mediator, IHttpContextAccessor httpContextAccessor)
        {
            _mediator = mediator;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Results<NoContent, BadRequest<ApiHttpErrorResponse>, StatusCodeHttpResult>> Execute(
            PurchaseSkinRequestDto request, CancellationToken cancellationToken = default)
        {
            var email = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Email);
            var result = await _mediator.Send(new PurchaseSkinCommand(email!, request.SkinId), cancellationToken);

            return result.Match<Results<NoContent, BadRequest<ApiHttpErrorResponse>, StatusCodeHttpResult>>(
                Right: _ => TypedResults.NoContent(),
                Left: error => error switch
                {
                    OwnerNotFoundError => TypedResults.BadRequest(new ApiHttpErrorResponse(
                        title: "Bad Request",
                        status: StatusCodes.Status400BadRequest,
                        errors: new List<ErrorDetail> { new("Email", "Owner not found") }
                    )),
                    SkinNotFoundError => TypedResults.BadRequest(new ApiHttpErrorResponse(
                        title: "Bad Request",
                        status: StatusCodes.Status400BadRequest,
                        errors: new List<ErrorDetail> { new("SkinId", "Skin not found") }
                    )),
                    SkinNotAvailableError => TypedResults.BadRequest(new ApiHttpErrorResponse(
                        title: "Bad Request",
                        status: StatusCodes.Status400BadRequest,
                        errors: new List<ErrorDetail> { new("SkinId", "Skin not available") }
                    )),
                    _ => TypedResults.StatusCode(StatusCodes.Status500InternalServerError)
                }
            );
        }
    }
}