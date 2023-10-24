namespace OpenSkinsApi.Tests.Modules.Skins.Domain.Entities
{
    using FluentAssertions;
    using OpenSkinsApi.Domain;
    using OpenSkinsApi.Modules.Skins.Domain.Entities;
    using OpenSkinsApi.Modules.Skins.Domain.Enums;
    using OpenSkinsApi.Modules.Skins.Domain.ValueObjects;
    using Xunit;
    public class SkinTests
    {
        [Theory]
        [ClassData(typeof(ExisitingSkinData))]
        public void Create_ValidSkin_ReturnsSkinInstance(UniqueIdentity id, Name name, Money price, Type type, Color color)
        {
            var skin = Skin.Create(id, name, price, type, color);

            skin.Should().NotBeNull();
            skin.Should().BeOfType<Skin>();
            skin.Name.Should().Be(name);
            skin.Price.Should().Be(price);
            skin.Type.Should().Be(type);
            skin.Color.Should().Be(color);
            skin.IsAvailable.Should().BeTrue();
        }


        [Theory]
        [ClassData(typeof(ExisitingSkinData))]
        public void MakeItUnavailable_SetsIsAvailableToFalse(UniqueIdentity id, Name name, Money price, Type type, Color color)
        {
            var skin = Skin.Create(id, name, price, type, color);

            skin.MakeItUnavailable();

            skin.IsAvailable.Should().BeFalse();
        }
    }

    public class ExisitingSkinData : TheoryData<UniqueIdentity, Name, Money, Type, Color>
    {
        public ExisitingSkinData()
        {

            var name = Name.Create("John Doe");
            var price = Money.Create(20.0m);
            var type = Type.Normal;
            var color = Color.Black;
            var id = new UniqueIdentity(null);

            Add(id, name, price, type, color);
        }
    }
}
