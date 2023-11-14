using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using OpenSkinsApi.Application.Core;
using OpenSkinsApi.Infrastructure.Http.Core;

namespace OpenSkinsApi.Modules.Users.Application.UpdateUser
{
    public class UpdateUserHttpController : IHttpController<
        UpdateUserHttpRequestDto,
        Results<Ok, BadRequest<ApiHttpErrorResponse>,
        StatusCodeHttpResult>>
    {
        private readonly IMediator _mediator;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UpdateUserHttpController(
            IMediator mediator,
            IHttpContextAccessor httpContextAccessor)
        {
            _mediator = mediator;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<Results<Ok, BadRequest<ApiHttpErrorResponse>, StatusCodeHttpResult>> Execute(UpdateUserHttpRequestDto request, CancellationToken cancellationToken = default)
        {
            var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _mediator.Send(new UpdateUserCommand(
                    userId!,
                    request.FirstName,
                    request.LastName)
                , cancellationToken);

            return result.Match<Results<Ok, BadRequest<ApiHttpErrorResponse>, StatusCodeHttpResult>>(
                Right: _ => TypedResults.Ok(),
                Left: error => error switch
                {
                    UserNotFoundError => TypedResults.BadRequest(
                        new ApiHttpErrorResponse(
                            "BadRequest",
                            StatusCodes.Status400BadRequest,
                            new List<ErrorDetail> { new("UserName", "User not found") }
                            )
                        ),
                    _ => TypedResults.StatusCode(StatusCodes.Status500InternalServerError)
                }
            );
        }
    }
}