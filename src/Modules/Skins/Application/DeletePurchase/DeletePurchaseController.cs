using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using OpenSkinsApi.Application.Core;
using OpenSkinsApi.Infrastructure.Http.Core;

namespace OpenSkinsApi.Modules.Skins.Application.DeletePurchase
{
    public class DeletePurchaseController : IHttpController
        <DeletePurchaseRequestDto, Results<NoContent, BadRequest<ApiHttpErrorResponse>, StatusCodeHttpResult>>
    {
        private readonly IMediator _mediator;
        public DeletePurchaseController(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<Results<NoContent, BadRequest<ApiHttpErrorResponse>, StatusCodeHttpResult>> Execute(DeletePurchaseRequestDto request, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(new DeleteOwnedSkinCommand(request.PurchaseId, request.OwnerEmail), cancellationToken);

            return result.Match<Results<NoContent, BadRequest<ApiHttpErrorResponse>, StatusCodeHttpResult>>(
                Right: _ => TypedResults.NoContent(),
                Left: error => error switch
                {
                    SkinNotOwnedError => TypedResults.BadRequest(new ApiHttpErrorResponse(
                        title: "Bad Request",
                        status: StatusCodes.Status400BadRequest,
                        errors: new List<ErrorDetail> { new("PurchaseId", "Skin not owned") }
                    )),
                    _ => TypedResults.StatusCode(StatusCodes.Status500InternalServerError)
                }
            );
        }
    }
}