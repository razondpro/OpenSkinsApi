using FluentAssertions;
using OpenSkinsApi.Domain;
using OpenSkinsApi.Modules.Skins.Domain.Entities;
using OpenSkinsApi.Modules.Skins.Domain.ValueObjects;
using Xunit;

namespace OpenSkinsApi.Tests.Modules.Skins.Domain.Entities
{
    public class UserTests
    {
        [Fact]
        public void Create_ValidUser_ReturnsUserInstance()
        {
            UniqueIdentity id = new(null);
            Email email = Email.Create("user@example.com");


            User user = User.Create(id, email);

            user.Should().NotBeNull();
            user.Should().BeOfType<User>();
            user.Email.Should().Be(email);
        }
    }
}
