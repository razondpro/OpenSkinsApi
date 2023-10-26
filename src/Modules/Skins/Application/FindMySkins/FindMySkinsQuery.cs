using LanguageExt;
using OpenSkinsApi.Application.Queries;
using OpenSkinsApi.Modules.Skins.Domain.Entities;

namespace OpenSkinsApi.Modules.Skins.Application.FindMySkins
{
    public record FindMySkinsQuery(string OwnerEmail) : IQuery<Either<Exception, List<Purchase>>>;
}