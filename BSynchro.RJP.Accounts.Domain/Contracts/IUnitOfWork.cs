using BSynchro.RJP.Accounts.Domain.Entities;

namespace BSynchro.RJP.Accounts.Domain.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity;
        Task<int> Save();
    }
}
