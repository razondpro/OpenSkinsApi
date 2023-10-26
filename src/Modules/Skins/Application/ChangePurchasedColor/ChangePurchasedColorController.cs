using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using OpenSkinsApi.Application.Core;
using OpenSkinsApi.Infrastructure.Http.Core;

namespace OpenSkinsApi.Modules.Skins.Application.ChangePurchasedColor
{
    public class ChangePurchasedColorController :
        IHttpController<ChangePurchasedColorRequestDto, Results<NoContent, BadRequest<ApiHttpErrorResponse>, StatusCodeHttpResult>>
    {
        private readonly IMediator _mediator;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ChangePurchasedColorController(IMediator mediator, IHttpContextAccessor httpContextAccessor)
        {
            _mediator = mediator;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<Results<NoContent, BadRequest<ApiHttpErrorResponse>, StatusCodeHttpResult>>
            Execute(ChangePurchasedColorRequestDto request, CancellationToken cancellationToken = default)
        {
            var email = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Email);
            var result = await _mediator.Send(
                new ChangePurchasedColorCommand(request.ColorNumber, request.PurchaseId, email!),
                cancellationToken
            );

            return result.Match<Results<NoContent, BadRequest<ApiHttpErrorResponse>, StatusCodeHttpResult>>(
                Right: _ => TypedResults.NoContent(),
                Left: error => error switch
                {
                    SkinNotOwnedError => TypedResults.BadRequest(new ApiHttpErrorResponse(
                        title: "Bad Request",
                        status: StatusCodes.Status400BadRequest,
                        errors: new List<ErrorDetail> { new("PurchaseId", "Skin not owned") }
                    )),
                    SkinAlreadyHasSameColorError => TypedResults.BadRequest(new ApiHttpErrorResponse(
                        title: "Bad Request",
                        status: StatusCodes.Status400BadRequest,
                        errors: new List<ErrorDetail> { new("ColorNumber", "Skin already has same color") }
                    )),
                    _ => TypedResults.StatusCode(StatusCodes.Status500InternalServerError)
                }
            );
        }
    }
}