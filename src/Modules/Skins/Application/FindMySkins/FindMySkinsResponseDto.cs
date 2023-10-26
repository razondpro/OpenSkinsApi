namespace OpenSkinsApi.Modules.Skins.Application.FindAvailableSkins
{
    using OpenSkinsApi.Infrastructure.Http.Core;
    using OpenSkinsApi.Modules.Skins.Domain.Entities;
    using OpenSkinsApi.Modules.Skins.Domain.Enums;
    public class FindMySkinsResponseDto : ApiHttpResponse
    {
        public List<MySkinsDto> Skins { get; init; }

        public FindMySkinsResponseDto(string title, int status, List<MySkinsDto> mySkins) : base(title, status)
        {
            Skins = mySkins;
        }

    }

    public class MySkinsDto
    {
        public string PurchaseId { get; init; }
        public string SkinId { get; init; }
        public string Name { get; init; }
        public decimal Price { get; init; }
        public Type Type { get; init; }
        public Color Color { get; init; }
        public string PurchasedOn { get; init; }

        public MySkinsDto(Purchase purchase)
        {
            PurchaseId = purchase.Id.Value.ToString();
            SkinId = purchase.SkinId.Value.ToString();
            Name = purchase.Skin.Name.Value;
            Price = purchase.Skin.Price.Amount;
            Type = purchase.Skin.Type;
            Color = purchase.Color;
            PurchasedOn = purchase.CreatedOn.ToString();
        }
    }
}