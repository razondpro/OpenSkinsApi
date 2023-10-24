using OpenSkinsApi.Application.Queries;
using OpenSkinsApi.Modules.Skins.Domain.Entities;

namespace OpenSkinsApi.Modules.Skins.Application.FindAvailableSkins
{
    public record FindAvailableSkinsQuery() : IQuery<List<Skin>>;
}