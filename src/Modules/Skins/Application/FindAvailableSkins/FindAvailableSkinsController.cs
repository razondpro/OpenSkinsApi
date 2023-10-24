using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using OpenSkinsApi.Application.Core;

namespace OpenSkinsApi.Modules.Skins.Application.FindAvailableSkins
{
    public class FindAvailableSkinsController :
        IHttpController<FindAvailableSkinsRequestDto, Ok<FindAvailableSkinsResponseDto>>
    {
        private readonly IMediator _mediator;

        public FindAvailableSkinsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<Ok<FindAvailableSkinsResponseDto>> Execute(FindAvailableSkinsRequestDto request, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(new FindAvailableSkinsQuery(), cancellationToken);

            var response = new FindAvailableSkinsResponseDto(result);

            return TypedResults.Ok(response);
        }
    }
}