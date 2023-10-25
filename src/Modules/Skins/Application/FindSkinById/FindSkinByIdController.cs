using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using OpenSkinsApi.Application.Core;

namespace OpenSkinsApi.Modules.Skins.Application.FindSkinById
{
    public class FindSkinByIdController : IHttpController
        <FindSkinByIdRequestDto, Results<Ok<FindSkinByIdResponseDto>, NotFound, StatusCodeHttpResult>>
    {
        private readonly IMediator _mediator;

        public FindSkinByIdController(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<Results<Ok<FindSkinByIdResponseDto>, NotFound, StatusCodeHttpResult>> Execute(FindSkinByIdRequestDto request, CancellationToken cancellationToken = default)
        {

            var result = await _mediator.Send(new FindSkinByIdQuery(request.Id), cancellationToken);

            return result.Match<Results<Ok<FindSkinByIdResponseDto>, NotFound, StatusCodeHttpResult>>(
               Right: skin => TypedResults.Ok(new FindSkinByIdResponseDto(
                     title: "Ok",
                     status: StatusCodes.Status200OK,
                     skin: skin
               )),
                Left: error => error switch
                {
                    SkinNotFoundByIdError => TypedResults.NotFound(),
                    _ => TypedResults.StatusCode(StatusCodes.Status500InternalServerError)
                }
            );

        }
    }
}