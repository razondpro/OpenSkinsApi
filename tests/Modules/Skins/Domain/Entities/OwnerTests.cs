using FluentAssertions;
using OpenSkinsApi.Domain;
using OpenSkinsApi.Modules.Skins.Domain.Entities;
using OpenSkinsApi.Modules.Skins.Domain.ValueObjects;
using Xunit;

namespace OpenSkinsApi.Tests.Modules.Skins.Domain.Entities
{
    public class OwnerTests
    {
        [Fact]
        public void Create_ValidOwner_ReturnsOwnerInstance()
        {
            UniqueIdentity id = new(null);
            Email email = Email.Create("owner@example.com");


            Owner owner = Owner.Create(id, email);

            owner.Should().NotBeNull();
            owner.Should().BeOfType<Owner>();
            owner.Email.Should().Be(email);
        }
    }
}
