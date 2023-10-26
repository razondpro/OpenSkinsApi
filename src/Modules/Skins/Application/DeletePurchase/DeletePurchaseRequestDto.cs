using Microsoft.AspNetCore.Mvc;

namespace OpenSkinsApi.Modules.Skins.Application.DeletePurchase
{
    public record DeletePurchaseRequestDto([FromRoute(Name = "purchaseId")] string PurchaseId);
}