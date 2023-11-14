namespace OpenSkinsApi.Modules.Auth.Application.Abstractions
{
    public interface IAuthUnitOfWork
    {
        Task CommitAsync(CancellationToken cancellationToken = default);
    }
}