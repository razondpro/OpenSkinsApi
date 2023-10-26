using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Moq;
using OpenSkinsApi.Infrastructure.Http.Core;
using OpenSkinsApi.Modules.Skins.Application.DeletePurchase;
using OpenSkinsApi.Tests.Base;
using Xunit;

namespace OpenSkinsApi.Tests.Modules.Skins.Application.DeletePurchase
{
    public class DeletePurchaseControllerTests : BaseTest
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly DeletePurchaseController _controller;

        public DeletePurchaseControllerTests(HttpContextFixture fixture) : base(fixture)
        {
            _mediatorMock = new Mock<IMediator>();
            _controller = new DeletePurchaseController(_mediatorMock.Object, _httpContextAccessorMock.Object);
        }

        [Fact]
        public async Task Execute_WhenDeleteOwnedSkinCommandSucceeds_ReturnsNoContent()
        {
            // Arrange
            var request = new DeletePurchaseRequestDto(Guid.NewGuid().ToString());
            _mediatorMock.Setup(x => x.Send(It.IsAny<DeleteOwnedSkinCommand>(), default)).ReturnsAsync(LanguageExt.Unit.Default);

            // Act
            var result = await _controller.Execute(request);

            // Assert
            result.Should().BeOfType<Results<NoContent, BadRequest<ApiHttpErrorResponse>, StatusCodeHttpResult>>();
            result.As<Results<NoContent, BadRequest<ApiHttpErrorResponse>, StatusCodeHttpResult>>()
                .Result.Should().BeEquivalentTo(TypedResults.NoContent());
        }

        [Fact]
        public async Task Execute_WhenSkinNotOwned_ReturnsBadRequestWithSkinNotOwnedError()
        {
            // Arrange
            var request = new DeletePurchaseRequestDto(Guid.NewGuid().ToString());
            _mediatorMock.Setup(x => x.Send(It.IsAny<DeleteOwnedSkinCommand>(), default)).ReturnsAsync(new SkinNotOwnedError());

            // Act
            var result = await _controller.Execute(request);

            // Assert
            result.Should().BeOfType<Results<NoContent, BadRequest<ApiHttpErrorResponse>, StatusCodeHttpResult>>();
            result.As<Results<NoContent, BadRequest<ApiHttpErrorResponse>, StatusCodeHttpResult>>()
                .Result.Should().BeEquivalentTo(TypedResults.BadRequest(new ApiHttpErrorResponse(
                    title: "Bad Request",
                    status: StatusCodes.Status400BadRequest,
                    errors: new List<ErrorDetail> { new("PurchaseId", "Skin not owned") }
                )));
        }

        [Fact]
        public async Task Execute_WhenDeleteOwnedSkinCommandFails_ReturnsInternalServerError()
        {
            // Arrange
            var request = new DeletePurchaseRequestDto(Guid.NewGuid().ToString());
            _mediatorMock.Setup(x => x.Send(It.IsAny<DeleteOwnedSkinCommand>(), default)).ReturnsAsync(new Exception());

            // Act
            var result = await _controller.Execute(request);

            // Assert
            result.Should().BeOfType<Results<NoContent, BadRequest<ApiHttpErrorResponse>, StatusCodeHttpResult>>();
            result.As<Results<NoContent, BadRequest<ApiHttpErrorResponse>, StatusCodeHttpResult>>()
                .Result.Should().BeEquivalentTo(TypedResults.StatusCode(StatusCodes.Status500InternalServerError));
        }
    }
}