using OpenSkinsApi.Application.Core;
using Microsoft.AspNetCore.Http.HttpResults;

namespace OpenSkinsApi.Modules.Auth.Application.LoginUser
{
    public class LoginUserHttpController : IHttpController
        <LoginUserRequestDto,
        Results<Ok<LoginUserResponseDto>, UnauthorizedHttpResult, ForbidHttpResult, StatusCodeHttpResult>>
    {
        private readonly IUseCase<LoginUserRequestDto, LoginUserUseCaseResult> _useCase;

        public LoginUserHttpController(IUseCase<LoginUserRequestDto, LoginUserUseCaseResult> useCase)
        {
            _useCase = useCase;
        }

        public async Task<Results<Ok<LoginUserResponseDto>, UnauthorizedHttpResult, ForbidHttpResult, StatusCodeHttpResult>>
        Execute(LoginUserRequestDto request, CancellationToken cancellationToken = default)
        {

            var result = await _useCase.Execute(request);

            return result.Match<Results<Ok<LoginUserResponseDto>, UnauthorizedHttpResult, ForbidHttpResult, StatusCodeHttpResult>>(
                Right: result => TypedResults.Ok(
                    new LoginUserResponseDto(
                        "Ok",
                        StatusCodes.Status200OK,
                        result
                    )
                ),
                Left: error => error switch
                {
                    PasswordMismatchError => TypedResults.Unauthorized(),
                    UserNotFoundError => TypedResults.Unauthorized(),
                    UserNotVerifiedError => TypedResults.StatusCode(StatusCodes.Status403Forbidden),
                    _ => TypedResults.StatusCode(StatusCodes.Status500InternalServerError)
                }
            );
        }
    }
}