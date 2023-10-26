using System.Security.Claims;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Moq;
using OpenSkinsApi.Infrastructure.Http.Core;
using OpenSkinsApi.Modules.Skins.Application.FindAvailableSkins;
using OpenSkinsApi.Modules.Skins.Application.FindMySkins;
using OpenSkinsApi.Modules.Skins.Domain.Entities;
using OpenSkinsApi.Tests.Base;
using Xunit;

namespace OpenSkinsApi.Tests.Modules.Skins.Application.FindMySkins
{
    public class FindMySkinsControllerTests : BaseTest
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly FindMySkinsController _controller;

        public FindMySkinsControllerTests(HttpContextFixture fixture) : base(fixture)
        {
            _mediatorMock = new Mock<IMediator>();
            _controller = new FindMySkinsController(_mediatorMock.Object, _httpContextAccessorMock.Object);
        }

        [Fact]
        public async Task Execute_WhenFindMySkinsQuerySucceeds_ReturnsOkWithFindMySkinsResponseDto()
        {
            // Arrange
            var request = new FindMySkinsRequestDto();
            var purchases = new List<Purchase>();
            _mediatorMock.Setup(x => x.Send(It.IsAny<FindMySkinsQuery>(), default)).ReturnsAsync(purchases);

            // Act
            var result = await _controller.Execute(request);

            // Assert
            result.Should().BeOfType<Results<Ok<FindMySkinsResponseDto>, BadRequest<ApiHttpErrorResponse>, StatusCodeHttpResult>>();
            result.As<Results<Ok<FindMySkinsResponseDto>, BadRequest<ApiHttpErrorResponse>, StatusCodeHttpResult>>()
                .Result.Should().BeEquivalentTo(TypedResults.Ok(new FindMySkinsResponseDto(
                    title: "Ok",
                    status: StatusCodes.Status200OK,
                    mySkins: purchases.ConvertAll(p => new MySkinsDto(p))
                )));
        }

        [Fact]
        public async Task Execute_WhenOwnerNotFound_ReturnsBadRequestWithOwnerNotFoundError()
        {
            // Arrange
            var request = new FindMySkinsRequestDto();
            _mediatorMock.Setup(x => x.Send(It.IsAny<FindMySkinsQuery>(), default)).ReturnsAsync(new OwnerNotFoundError());

            // Act
            var result = await _controller.Execute(request);

            // Assert
            result.Should().BeOfType<Results<Ok<FindMySkinsResponseDto>, BadRequest<ApiHttpErrorResponse>, StatusCodeHttpResult>>();
            result.As<Results<Ok<FindMySkinsResponseDto>, BadRequest<ApiHttpErrorResponse>, StatusCodeHttpResult>>()
                .Result.Should().BeEquivalentTo(TypedResults.BadRequest(new ApiHttpErrorResponse(
                    title: "Bad Request",
                    status: StatusCodes.Status400BadRequest,
                    errors: new List<ErrorDetail> { new("Email", "Owner not found") }
                )));
        }

        [Fact]
        public async Task Execute_WhenFindMySkinsQueryFails_ReturnsInternalServerError()
        {
            // Arrange
            var request = new FindMySkinsRequestDto();
            _mediatorMock.Setup(x => x.Send(It.IsAny<FindMySkinsQuery>(), default)).ReturnsAsync(new Exception());

            // Act
            var result = await _controller.Execute(request);

            // Assert
            result.Should().BeOfType<Results<Ok<FindMySkinsResponseDto>, BadRequest<ApiHttpErrorResponse>, StatusCodeHttpResult>>();
            result.As<Results<Ok<FindMySkinsResponseDto>, BadRequest<ApiHttpErrorResponse>, StatusCodeHttpResult>>()
                .Result.Should().BeEquivalentTo(TypedResults.StatusCode(StatusCodes.Status500InternalServerError));
        }
    }
}