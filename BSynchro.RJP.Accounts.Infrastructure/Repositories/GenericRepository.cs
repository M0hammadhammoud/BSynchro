using BSynchro.RJP.Accounts.Domain.Contracts;
using BSynchro.RJP.Accounts.Domain.Entities;
using BSynchro.RJP.Accounts.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BSynchro.RJP.Accounts.Infrastructure.Repositories
{
    //generic repository is the class that defines base CRUD operations over our repositories
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly CoreDBContext _coreDBContext;

        public GenericRepository(CoreDBContext coreDBContext)
        {
            _coreDBContext = coreDBContext;
        }

        public void DeleteAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            try
            {
                return await _coreDBContext.Set<T>().ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<T> GetByIdAsync(int id)
        {
            try
            {
                return await _coreDBContext.Set<T>().FindAsync(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void UpdateAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public void Add(T entity)
        {
            _coreDBContext.Add<T>(entity);
        }

        public void Update(T entity)
        {
            _coreDBContext.Attach<T>(entity);
            _coreDBContext.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(T entity)
        {
            _coreDBContext.Set<T>().Remove(entity);
        }
    }
}
