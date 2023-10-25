namespace OpenSkinsApi.Tests.Modules.Skins.Application.FindSkinById
{
    using FluentAssertions;
    using LanguageExt;
    using MediatR;
    using Microsoft.AspNetCore.Http.HttpResults;
    using Moq;
    using OpenSkinsApi.Modules.Skins.Application.FindSkinById;
    using OpenSkinsApi.Modules.Skins.Domain.Entities;
    using OpenSkinsApi.Modules.Skins.Domain.Enums;
    using OpenSkinsApi.Modules.Skins.Domain.ValueObjects;
    using Xunit;

    public class FindSkinByIdControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock;

        public FindSkinByIdControllerTests()
        {
            _mediatorMock = new();
        }

        [Fact]
        public async void Execute_Should_Return_Ok_When_Mediator_Returns_Skin()
        {
            // Arrange
            var request = new FindSkinByIdRequestDto(Guid.NewGuid().ToString());
            var controller = new FindSkinByIdController(_mediatorMock.Object);
            var skin = Skin.Create(null, Name.Create("Skin 1"), Money.Create(10.0m), Type.Rare, Color.Red);

            _mediatorMock.Setup(x => x.Send(It.IsAny<FindSkinByIdQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Either<Exception, Skin>.Right(skin));

            // Act
            var result = await controller.Execute(request, CancellationToken.None);

            // Assert
            result.Should().BeOfType<Results<Ok<FindSkinByIdResponseDto>, NotFound, StatusCodeHttpResult>>();
            result.As<Results<Ok<FindSkinByIdResponseDto>, NotFound, StatusCodeHttpResult>>()
                .Result.Should().BeEquivalentTo(TypedResults.Ok(new FindSkinByIdResponseDto(
                    title: "Ok",
                    status: StatusCodes.Status200OK,
                    skin: skin
                )));
        }
        [Fact]
        public async void Execute_Should_Return_NotFound_When_Mediator_Returns_SkinNotFoundByIdError()
        {
            // Arrange
            var request = new FindSkinByIdRequestDto(Guid.NewGuid().ToString());
            var controller = new FindSkinByIdController(_mediatorMock.Object);

            _mediatorMock.Setup(x => x.Send(It.IsAny<FindSkinByIdQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Either<Exception, Skin>.Left(new SkinNotFoundByIdError()));

            // Act
            var result = await controller.Execute(request, CancellationToken.None);

            // Assert
            result.Should().BeOfType<Results<Ok<FindSkinByIdResponseDto>, NotFound, StatusCodeHttpResult>>();
            result.As<Results<Ok<FindSkinByIdResponseDto>, NotFound, StatusCodeHttpResult>>()
                .Result.Should().BeEquivalentTo(TypedResults.NotFound());
        }

        [Fact]
        public async void Execute_Should_Return_InternalServerError_When_Mediator_Returns_Exception()
        {
            // Arrange
            var request = new FindSkinByIdRequestDto(Guid.NewGuid().ToString());
            var controller = new FindSkinByIdController(_mediatorMock.Object);

            _mediatorMock.Setup(x => x.Send(It.IsAny<FindSkinByIdQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Either<Exception, Skin>.Left(new Exception("Test exception")));

            // Act
            var result = await controller.Execute(request, CancellationToken.None);

            // Assert
            result.Should().BeOfType<Results<Ok<FindSkinByIdResponseDto>, NotFound, StatusCodeHttpResult>>();
            result.As<Results<Ok<FindSkinByIdResponseDto>, NotFound, StatusCodeHttpResult>>()
                .Result.Should().BeEquivalentTo(TypedResults.StatusCode(StatusCodes.Status500InternalServerError));
        }
    }
}