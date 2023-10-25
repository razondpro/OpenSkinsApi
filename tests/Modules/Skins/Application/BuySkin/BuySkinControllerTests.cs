using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Moq;
using OpenSkinsApi.Infrastructure.Http.Core;
using OpenSkinsApi.Modules.Skins.Application.BuySkin;
using Xunit;

namespace OpenSkinsApi.Tests.Modules.Skins.Application.BuySkin
{
    public class BuySkinControllerTests
    {

        private readonly Mock<IMediator> _mediatorMock;
        private readonly BuySkinController _controller;

        public BuySkinControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _controller = new BuySkinController(_mediatorMock.Object);
        }

        [Fact]
        public async Task Execute_WhenBuySkinCommandSucceeds_ReturnsNoContent()
        {
            // Arrange
            var request = new BuySkinRequestDto("test@example.com", Guid.NewGuid().ToString());
            _mediatorMock.Setup(x => x.Send(It.IsAny<BuySkinCommand>(), default)).ReturnsAsync(LanguageExt.Unit.Default);

            // Act
            var result = await _controller.Execute(request);

            // Assert
            result.Should().BeOfType<Results<NoContent, BadRequest<ApiHttpErrorResponse>, StatusCodeHttpResult>>();
            result.As<Results<NoContent, BadRequest<ApiHttpErrorResponse>, StatusCodeHttpResult>>()
                .Result.Should().BeEquivalentTo(TypedResults.NoContent());
        }


        [Fact]
        public async Task Execute_WhenUserNotFound_ReturnsBadRequestWithUserNotFoundError()
        {
            // Arrange
            var request = new BuySkinRequestDto("test@example.com", Guid.NewGuid().ToString());
            _mediatorMock.Setup(x => x.Send(It.IsAny<BuySkinCommand>(), default)).ReturnsAsync(new UserNotFoundError());

            // Act
            var result = await _controller.Execute(request);

            // Assert
            result.Should().BeOfType<Results<NoContent, BadRequest<ApiHttpErrorResponse>, StatusCodeHttpResult>>();
            result.As<Results<NoContent, BadRequest<ApiHttpErrorResponse>, StatusCodeHttpResult>>()
                .Result.Should().BeEquivalentTo(TypedResults.BadRequest(new ApiHttpErrorResponse(
                    title: "Bad Request",
                    status: StatusCodes.Status400BadRequest,
                    errors: new List<ErrorDetail> { new("Email", "User not found") }
                )));
        }

        [Fact]
        public async Task Execute_WhenSkinNotFound_ReturnsBadRequestWithSkinNotFoundError()
        {
            // Arrange
            var request = new BuySkinRequestDto("test@example.com", Guid.NewGuid().ToString());
            _mediatorMock.Setup(x => x.Send(It.IsAny<BuySkinCommand>(), default)).ReturnsAsync(new SkinNotFoundError());

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
        public async Task Execute_WhenBuySkinCommandFails_ReturnsInternalServerError()
        {
            // Arrange
            var request = new BuySkinRequestDto("test@example.com", Guid.NewGuid().ToString());
            _mediatorMock.Setup(x => x.Send(It.IsAny<BuySkinCommand>(), default)).ReturnsAsync(new Exception());

            // Act
            var result = await _controller.Execute(request);

            // Assert
            result.Should().BeOfType<Results<NoContent, BadRequest<ApiHttpErrorResponse>, StatusCodeHttpResult>>();
            result.As<Results<NoContent, BadRequest<ApiHttpErrorResponse>, StatusCodeHttpResult>>()
                .Result.Should().BeEquivalentTo(TypedResults.StatusCode(StatusCodes.Status500InternalServerError));

        }
    }
}