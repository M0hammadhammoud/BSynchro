using BSynchro.RJP.Accounts.Domain.Contracts;
using BSynchro.RJP.Accounts.Domain.Entities;
using BSynchro.RJP.Accounts.Infrastructure.Repositories;
using System.Collections;

namespace BSynchro.RJP.Accounts.Infrastructure.Data
{
    //unit of work manages our repositories and their transactions over the db
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CoreDBContext _coreDBContext;
        private Hashtable _repositories;

        public UnitOfWork(CoreDBContext coreDBContext)
        {
            _coreDBContext = coreDBContext;
        }

        public async Task<int> Save()
        {
            return await _coreDBContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _coreDBContext.Dispose();
        }

        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            _repositories ??= [];
            var Type = typeof(TEntity).Name;

            if (!_repositories.ContainsKey(Type))
            {
                var repositiryType = typeof(GenericRepository<>);
                var repositoryInstance = Activator.CreateInstance(
                    repositiryType.MakeGenericType(typeof(TEntity)), _coreDBContext);
                _repositories.Add(Type, repositoryInstance);
            }

            return _repositories[Type] as IGenericRepository<TEntity>;
        }
    }
}
