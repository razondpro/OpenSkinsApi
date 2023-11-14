using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using OpenSkinsApi.Application.Core;
using OpenSkinsApi.Infrastructure.Http.Core;

namespace OpenSkinsApi.Modules.Auth.Application.RegisterUser
{
    public class RegisterUserHttpController :
        IHttpController<RegisterUserRequestDto, Results<Created, Conflict<ApiHttpErrorResponse>, StatusCodeHttpResult>>
    {
        private readonly IMediator _mediator;
        private readonly IHttpContextAccessor _httpContext;

        public RegisterUserHttpController(IMediator mediator, IHttpContextAccessor httpContext)
        {
            this._mediator = mediator;
            this._httpContext = httpContext;
        }

        public async Task<Results<Created, Conflict<ApiHttpErrorResponse>, StatusCodeHttpResult>> Execute(
             RegisterUserRequestDto request,
             CancellationToken cancellationToken = default)
        {

            var result = await _mediator.Send(new RegisterUserCommand(
                    request.FirstName,
                    request.LastName,
                    request.Email,
                    request.UserName,
                    request.Password)
                , cancellationToken);

            return result.Match<Results<Created, Conflict<ApiHttpErrorResponse>, StatusCodeHttpResult>>(
                Right: _ => TypedResults.Created($"{_httpContext.HttpContext?.Request.Path}/{request.Email}"),
                Left: error => error switch
                {
                    EmailAlreadyExistsError => TypedResults.Conflict(
                        new ApiHttpErrorResponse(
                            "Conflict",
                            StatusCodes.Status409Conflict,
                            new List<ErrorDetail> { new("Email", "Email already exists") }
                            )
                        ),
                    UserNameAlreadyExistsError => TypedResults.Conflict(
                        new ApiHttpErrorResponse(
                            "Conflict",
                            StatusCodes.Status409Conflict,
                            new List<ErrorDetail> { new("UserName", "UserName already exists") }
                            )
                        ),
                    _ => TypedResults.StatusCode(StatusCodes.Status500InternalServerError)
                }
            );
        }
    }
}