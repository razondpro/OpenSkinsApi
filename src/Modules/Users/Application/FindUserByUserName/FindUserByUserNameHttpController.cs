using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using OpenSkinsApi.Application.Core;

namespace OpenSkinsApi.Modules.Users.Application.FindUserByUserName
{
    public class FindUserByUserNameHttpController :
        IHttpController<FindUserByUserNameHttpRequestDto, Results<Ok<FindUserByUserNameHttpResponseDto>, NotFound, StatusCodeHttpResult>>
    {
        private readonly IMediator _mediator;

        public FindUserByUserNameHttpController(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<Results<Ok<FindUserByUserNameHttpResponseDto>, NotFound, StatusCodeHttpResult>> Execute(
            FindUserByUserNameHttpRequestDto request,
            CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(new FindUserByUserNameQuery(request.UserName), cancellationToken);

            return result.Match<Results<Ok<FindUserByUserNameHttpResponseDto>, NotFound, StatusCodeHttpResult>>(
                Right: user => TypedResults.Ok(new FindUserByUserNameHttpResponseDto(user)),
                Left: error => error switch
                {
                    UserNotFoundByEmailError => TypedResults.NotFound(),
                    _ => TypedResults.StatusCode(StatusCodes.Status500InternalServerError)
                }
            );
        }
    }
}