using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Moq;
using OpenSkinsApi.Infrastructure.Http.Core;
using OpenSkinsApi.Modules.Skins.Application.ChangePurchasedColor;
using OpenSkinsApi.Tests.Base;
using Xunit;

namespace OpenSkinsApi.Tests.Modules.Skins.Application.ChangePurchasedColor
{
    public class ChangePurchasedColorControllerTests : BaseTest
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly ChangePurchasedColorController _controller;

        public ChangePurchasedColorControllerTests(HttpContextFixture fixture) : base(fixture)
        {
            _mediatorMock = new Mock<IMediator>();
            _controller = new ChangePurchasedColorController(_mediatorMock.Object, _httpContextAccessorMock.Object);
        }

        [Fact]
        public async Task Execute_WhenCommandSucceeds_ReturnsNoContent()
        {
            // Arrange
            var purchaseId = "123";
            var colorNumber = 0;
            var request = new ChangePurchasedColorRequestDto(colorNumber, purchaseId);
            _mediatorMock.Setup(x => x.Send(It.IsAny<ChangePurchasedColorCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(LanguageExt.Unit.Default);

            // Act
            var result = await _controller.Execute(request, CancellationToken.None);

            // Assert
            result.Should().BeOfType<Results<NoContent, BadRequest<ApiHttpErrorResponse>, StatusCodeHttpResult>>();
            result.As<Results<NoContent, BadRequest<ApiHttpErrorResponse>, StatusCodeHttpResult>>()
                .Result.Should().BeEquivalentTo(TypedResults.NoContent());
        }

        [Fact]
        public async Task Execute_WhenSkinNotOwnedError_ReturnsBadRequest()
        {
            // Arrange
            var purchaseId = "123";
            var colorNumber = 0;
            var request = new ChangePurchasedColorRequestDto(colorNumber, purchaseId);
            _mediatorMock.Setup(x => x.Send(It.IsAny<ChangePurchasedColorCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(new SkinNotOwnedError());

            // Act
            var result = await _controller.Execute(request, CancellationToken.None);

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
        public async Task Execute_WhenSkinAlreadyHasSameColorError_ReturnsBadRequest()
        {
            // Arrange
            var purchaseId = "123";
            var colorNumber = 0;
            var request = new ChangePurchasedColorRequestDto(colorNumber, purchaseId);
            _mediatorMock.Setup(x => x.Send(It.IsAny<ChangePurchasedColorCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(new SkinAlreadyHasSameColorError());

            // Act
            var result = await _controller.Execute(request, CancellationToken.None);

            // Assert
            result.Should().BeOfType<Results<NoContent, BadRequest<ApiHttpErrorResponse>, StatusCodeHttpResult>>();
            result.As<Results<NoContent, BadRequest<ApiHttpErrorResponse>, StatusCodeHttpResult>>()
                .Result.Should().BeEquivalentTo(TypedResults.BadRequest(new ApiHttpErrorResponse(
                    title: "Bad Request",
                    status: StatusCodes.Status400BadRequest,
                    errors: new List<ErrorDetail> { new("ColorNumber", "Skin already has same color") }
                )));
        }

        [Fact]
        public async Task Execute_WhenCommandFails_ReturnsInternalServerError()
        {
            // Arrange
            var purchaseId = "123";
            var colorNumber = 0;
            var request = new ChangePurchasedColorRequestDto(colorNumber, purchaseId);
            _mediatorMock.Setup(x => x.Send(It.IsAny<ChangePurchasedColorCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(new Exception());

            // Act
            var result = await _controller.Execute(request, CancellationToken.None);

            // Assert
            result.Should().BeOfType<Results<NoContent, BadRequest<ApiHttpErrorResponse>, StatusCodeHttpResult>>();
            result.As<Results<NoContent, BadRequest<ApiHttpErrorResponse>, StatusCodeHttpResult>>()
                .Result.Should().BeEquivalentTo(TypedResults.StatusCode(StatusCodes.Status500InternalServerError));
        }
    }
}