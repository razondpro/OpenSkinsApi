namespace OpenSkinsApi.Modules.Skins.Application.FindSkinById
{
    using OpenSkinsApi.Infrastructure.Http.Core;
    using OpenSkinsApi.Modules.Skins.Domain.Entities;
    using OpenSkinsApi.Modules.Skins.Domain.Enums;
    public class FindSkinByIdResponseDto : ApiHttpResponse
    {
        public SkinDto Skin { get; init; }
        public FindSkinByIdResponseDto(string title, int status, Skin skin) : base(title, status)
        {
            Skin = new SkinDto(skin);
        }
    }

    public class SkinDto
    {
        public string Id { get; init; }
        public string Name { get; init; }
        public decimal Price { get; init; }
        public Type Type { get; init; }
        public Color Color { get; init; }

        public SkinDto(Skin skin)
        {
            Id = skin.Id.Value.ToString();
            Name = skin.Name.Value;
            Price = skin.Price.Amount;
            Type = skin.Type;
            Color = skin.Color;
        }
    }
}