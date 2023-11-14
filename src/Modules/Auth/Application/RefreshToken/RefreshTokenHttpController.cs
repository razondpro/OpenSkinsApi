using OpenSkinsApi.Modules.Auth.Application.RefreshToken;
using OpenSkinsApi.Application.Core;
using Microsoft.AspNetCore.Http.HttpResults;

namespace OpenSkinsApi.Modules.Auth.Application.RefreshToken
{
    public class RefreshTokenHttpController : IHttpController
        <RefreshTokenRequestDto,
        Results<Ok<RefreshTokenResponseDto>, UnauthorizedHttpResult, StatusCodeHttpResult>>
    {

        private readonly IUseCase<RefreshTokenRequestDto, RefreshTokenUseCaseResult> _useCase;

        public RefreshTokenHttpController(IUseCase<RefreshTokenRequestDto, RefreshTokenUseCaseResult> useCase)
        {
            _useCase = useCase;
        }

        public async Task<Results<Ok<RefreshTokenResponseDto>, UnauthorizedHttpResult, StatusCodeHttpResult>>
        Execute(RefreshTokenRequestDto request, CancellationToken cancellationToken = default)
        {
            var result = await _useCase.Execute(request);

            return result.Match<Results<Ok<RefreshTokenResponseDto>, UnauthorizedHttpResult, StatusCodeHttpResult>>(
                Right: result => TypedResults.Ok(
                    new RefreshTokenResponseDto(
                        "Ok",
                        StatusCodes.Status200OK,
                        result
                    )
                ),
                Left: error => error switch
                {
                    RefreshTokenNotFoundError => TypedResults.Unauthorized(),
                    InvalidRefreshTokenError => TypedResults.Unauthorized(),
                    _ => TypedResults.StatusCode(StatusCodes.Status500InternalServerError)
                }
            );

        }
    }
}