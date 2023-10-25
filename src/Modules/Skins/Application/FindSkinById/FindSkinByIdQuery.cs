using LanguageExt;
using OpenSkinsApi.Application.Queries;
using OpenSkinsApi.Modules.Skins.Domain.Entities;

namespace OpenSkinsApi.Modules.Skins.Application.FindSkinById
{
    public record FindSkinByIdQuery(string Id) : IQuery<Either<Exception, Skin>>;
}