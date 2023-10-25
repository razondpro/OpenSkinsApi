using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using OpenSkinsApi.Application.Core;
using OpenSkinsApi.Infrastructure.Http.Core;

namespace OpenSkinsApi.Modules.Skins.Application.BuySkin
{
    public class BuySkinController :
        IHttpController<BuySkinRequestDto, Results<NoContent, BadRequest<ApiHttpErrorResponse>, StatusCodeHttpResult>>
    {
        private readonly IMediator _mediator;
        public BuySkinController(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<Results<NoContent, BadRequest<ApiHttpErrorResponse>, StatusCodeHttpResult>> Execute(
            BuySkinRequestDto request, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(new BuySkinCommand(request.Email, request.SkinId), cancellationToken);

            return result.Match<Results<NoContent, BadRequest<ApiHttpErrorResponse>, StatusCodeHttpResult>>(
                Right: _ => TypedResults.NoContent(),
                Left: error => error switch
                {
                    UserNotFoundError => TypedResults.BadRequest(new ApiHttpErrorResponse(
                        title: "Bad Request",
                        status: StatusCodes.Status400BadRequest,
                        errors: new List<ErrorDetail> { new("Email", "User not found") }
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