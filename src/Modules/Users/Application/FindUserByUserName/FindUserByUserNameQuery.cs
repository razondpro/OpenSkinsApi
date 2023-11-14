namespace OpenSkinsApi.Modules.Users.Application.FindUserByUserName
{
    using LanguageExt;
    using OpenSkinsApi.Modules.Users.Domain.Entities;
    using OpenSkinsApi.Application.Queries;

    public sealed record FindUserByUserNameQuery(string UserName) : IQuery<Either<Exception, User>>;

}