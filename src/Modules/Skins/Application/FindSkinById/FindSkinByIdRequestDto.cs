using Microsoft.AspNetCore.Mvc;

namespace OpenSkinsApi.Modules.Skins.Application.FindSkinById
{
    public record FindSkinByIdRequestDto([FromRoute(Name = "id")] string Id);
}