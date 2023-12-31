namespace OpenSkinsApi.Infrastructure.Persistence.Core.UnitOfWork
{

    public class UnitOfWork : IUnitOfWork
    {

        private readonly Database DbContext;

        public UnitOfWork(Database database)
        {
            DbContext = database;
        }
        public async Task CommitAsync(CancellationToken cancellationToken = default)
        {
            await DbContext.SaveChangesAsync(cancellationToken);
        }

    }
}