using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using OpenSkinsApi.Application.Core;
using OpenSkinsApi.Infrastructure.Http.Core;
using OpenSkinsApi.Modules.Skins.Application.FindAvailableSkins;

namespace OpenSkinsApi.Modules.Skins.Application.FindMySkins
{
    public class FindMySkinsController : IHttpController<FindMySkinsRequestDto, Results<Ok<FindMySkinsResponseDto>, BadRequest<ApiHttpErrorResponse>, StatusCodeHttpResult>>
    {
        private readonly IMediator _mediator;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public FindMySkinsController(IMediator mediator, IHttpContextAccessor httpContextAccessor)
        {
            _mediator = mediator;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<Results<Ok<FindMySkinsResponseDto>, BadRequest<ApiHttpErrorResponse>, StatusCodeHttpResult>> Execute(FindMySkinsRequestDto request, CancellationToken cancellationToken = default)
        {
            var email = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Email);
            var result = await _mediator.Send(new FindMySkinsQuery(email!), cancellationToken);

            return result.Match<Results<Ok<FindMySkinsResponseDto>, BadRequest<ApiHttpErrorResponse>, StatusCodeHttpResult>>(
                Right: purchases => TypedResults.Ok(
                        new FindMySkinsResponseDto(
                            "Ok",
                            StatusCodes.Status200OK,
                            purchases.Select(p => new MySkinsDto(p)).ToList()
                        )
                ),
                Left: error => error switch
                {
                    OwnerNotFoundError => TypedResults.BadRequest(new ApiHttpErrorResponse(
                        title: "Bad Request",
                        status: StatusCodes.Status400BadRequest,
                        errors: new List<ErrorDetail> { new("Email", "Owner not found") }
                    )),
                    _ => TypedResults.StatusCode(StatusCodes.Status500InternalServerError)
                }
            );
        }
    }
}