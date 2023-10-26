namespace OpenSkinsApi.Modules.Skins.Application.ChangePurchasedColor
{
    public record ChangePurchasedColorRequestDto(
        int ColorNumber,
        string PurchaseId
    );
}