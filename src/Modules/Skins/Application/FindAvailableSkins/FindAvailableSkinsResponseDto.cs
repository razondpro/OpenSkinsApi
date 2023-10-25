namespace OpenSkinsApi.Modules.Skins.Application.FindAvailableSkins
{
    using OpenSkinsApi.Infrastructure.Http.Core;
    using OpenSkinsApi.Modules.Skins.Domain.Entities;
    using OpenSkinsApi.Modules.Skins.Domain.Enums;
    public class FindAvailableSkinsResponseDto : ApiHttpResponse
    {
        public List<AvailableSkinDto> Skins { get; init; }

        public FindAvailableSkinsResponseDto(List<Skin> skins) : base("Ok", StatusCodes.Status200OK)
        {
            Skins = MapSkinsToReponse(skins);
        }
        private static List<AvailableSkinDto> MapSkinsToReponse(List<Skin> skins)
        {
            var data = new List<AvailableSkinDto>();
            skins.ForEach(skin =>
            {
                data.Add(new AvailableSkinDto(
                    Id: skin.Id.Value.ToString(),
                    Name: skin.Name.Value,
                    Price: skin.Price.Amount,
                    Type: skin.Type,
                    Color: skin.Color
                ));
            });

            return data;
        }
    }

    public record AvailableSkinDto(
        string Id,
        string Name,
        decimal Price,
        Type Type,
        Color Color
    );
}