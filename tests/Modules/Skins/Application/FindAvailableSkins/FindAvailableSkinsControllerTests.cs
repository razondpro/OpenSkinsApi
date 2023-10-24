using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Moq;
using OpenSkinsApi.Modules.Skins.Application.FindAvailableSkins;
using OpenSkinsApi.Modules.Skins.Domain.Entities;
using Xunit;

namespace OpenSkinsApi.Tests.Modules.Skins.Application.FindAvailableSkins
{
    public class FindAvailableSkinsControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock;

        public FindAvailableSkinsControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
        }

        [Fact]
        public async Task Execute_Should_Return_Ok_With_Skins()
        {
            var skins = new List<Skin>();
            _mediatorMock.Setup(x => x.Send(It.IsAny<FindAvailableSkinsQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(skins);

            var controller = new FindAvailableSkinsController(_mediatorMock.Object);

            var result = await controller.Execute(new FindAvailableSkinsRequestDto());

            result.Should().BeOfType<Ok<FindAvailableSkinsResponseDto>>();
            result.Value.Should().BeOfType<FindAvailableSkinsResponseDto>();
        }

    }
}