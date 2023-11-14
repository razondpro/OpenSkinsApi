using OpenSkinsApi.Application.Core;
using OpenSkinsApi.Infrastructure.Http.Core;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace OpenSkinsApi.Modules.Auth.Application.RevokeToken
{
    public class RevokeTokenHttpController :
    IHttpController<RevokeTokenRequestDto, Results<Ok, BadRequest<ApiHttpErrorResponse>, StatusCodeHttpResult>>
    {

        private readonly IMediator _mediator;

        public RevokeTokenHttpController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        public async Task<Results<Ok, BadRequest<ApiHttpErrorResponse>, StatusCodeHttpResult>> Execute(RevokeTokenRequestDto request, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(new RevokeTokenCommand(request.RefreshToken), cancellationToken);

            return result.Match<Results<Ok, BadRequest<ApiHttpErrorResponse>, StatusCodeHttpResult>>(
                Right: _ => TypedResults.Ok(),
                Left: error => error switch
                {
                    RefreshTokenNotFoundError => TypedResults.BadRequest(
                        new ApiHttpErrorResponse(
                            "Bad Request",
                            StatusCodes.Status400BadRequest,
                            new List<ErrorDetail> { new("RefreshToken", "RefreshToken not found") }
                            )
                        ),
                    InvalidRefreshTokenError => TypedResults.BadRequest(
                        new ApiHttpErrorResponse(
                            "Bad Request",
                            StatusCodes.Status400BadRequest,
                            new List<ErrorDetail> { new("RefreshToken", "RefreshToken is invalid") }
                            )
                        ),
                    _ => TypedResults.StatusCode(StatusCodes.Status500InternalServerError)
                }
            );

        }
    }
}