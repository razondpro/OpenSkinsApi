using System.Security.Claims;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Moq;
using OpenSkinsApi.Infrastructure.Http.Core;
using OpenSkinsApi.Modules.Skins.Application.PurchaseSkin;
using OpenSkinsApi.Tests.Base;
using Xunit;

namespace OpenSkinsApi.Tests.Modules.Skins.Application.PurchaseSkin
{
    public class PurchaseSkinControllerTests : BaseTest
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly PurchaseSkinController _controller;

        public PurchaseSkinControllerTests(HttpContextFixture fixture) : base(fixture)
        {
            _mediatorMock = new Mock<IMediator>();
            _controller = new PurchaseSkinController(_mediatorMock.Object, _httpContextAccessorMock.Object);
        }

        [Fact]
        public async Task Execute_WhenPurchaseSkinCommandSucceeds_ReturnsNoContent()
        {
            // Arrange
            var request = new PurchaseSkinRequestDto(Guid.NewGuid().ToString());
            _mediatorMock.Setup(x => x.Send(It.IsAny<PurchaseSkinCommand>(), default)).ReturnsAsync(LanguageExt.Unit.Default);

            // Act
            var result = await _controller.Execute(request);

            // Assert
            result.Should().BeOfType<Results<NoContent, BadRequest<ApiHttpErrorResponse>, StatusCodeHttpResult>>();
            result.As<Results<NoContent, BadRequest<ApiHttpErrorResponse>, StatusCodeHttpResult>>()
                .Result.Should().BeEquivalentTo(TypedResults.NoContent());
        }


        [Fact]
        public async Task Execute_WhenOwnerNotFound_ReturnsBadRequestWithOwnerNotFoundError()
        {
            // Arrange
            var request = new PurchaseSkinRequestDto(Guid.NewGuid().ToString());
            _mediatorMock.Setup(x => x.Send(It.IsAny<PurchaseSkinCommand>(), default)).ReturnsAsync(new OwnerNotFoundError());

            // Act
            var result = await _controller.Execute(request);

            // Assert
            result.Should().BeOfType<Results<NoContent, BadRequest<ApiHttpErrorResponse>, StatusCodeHttpResult>>();
            result.As<Results<NoContent, BadRequest<ApiHttpErrorResponse>, StatusCodeHttpResult>>()
                .Result.Should().BeEquivalentTo(TypedResults.BadRequest(new ApiHttpErrorResponse(
                    title: "Bad Request",
                    status: StatusCodes.Status400BadRequest,
                    errors: new List<ErrorDetail> { new("Email", "Owner not found") }
                )));
        }

        [Fact]
        public async Task Execute_WhenSkinNotFound_ReturnsBadRequestWithSkinNotFoundError()
        {
            // Arrange
            var request = new PurchaseSkinRequestDto(Guid.NewGuid().ToString());
            _mediatorMock.Setup(x => x.Send(It.IsAny<PurchaseSkinCommand>(), default)).ReturnsAsync(new SkinNotFoundError());

            // Act
            var result = await _controller.Execute(request);

            // Assert
            result.Should().BeOfType<Results<NoContent, BadRequest<ApiHttpErrorResponse>, StatusCodeHttpResult>>();
            result.As<Results<NoContent, BadRequest<ApiHttpErrorResponse>, StatusCodeHttpResult>>()
                .Result.Should().BeEquivalentTo(TypedResults.BadRequest(new ApiHttpErrorResponse(
                    title: "Bad Request",
                    status: StatusCodes.Status400BadRequest,
                    errors: new List<ErrorDetail> { new("SkinId", "Skin not found") }
                )));
        }

        [Fact]
        public async Task Execute_WhenPurchaseSkinCommandFails_ReturnsInternalServerError()
        {
            // Arrange
            var request = new PurchaseSkinRequestDto(Guid.NewGuid().ToString());
            _mediatorMock.Setup(x => x.Send(It.IsAny<PurchaseSkinCommand>(), default)).ReturnsAsync(new Exception());

            // Act
            var result = await _controller.Execute(request);

            // Assert
            result.Should().BeOfType<Results<NoContent, BadRequest<ApiHttpErrorResponse>, StatusCodeHttpResult>>();
            result.As<Results<NoContent, BadRequest<ApiHttpErrorResponse>, StatusCodeHttpResult>>()
                .Result.Should().BeEquivalentTo(TypedResults.StatusCode(StatusCodes.Status500InternalServerError));

        }
    }
}