namespace OpenSkinsApi.Infrastructure.Persistence.Core.UnitOfWork
{
    public interface IUnitOfWork
    {
        Task CommitAsync(CancellationToken cancellationToken = default);
    }
}