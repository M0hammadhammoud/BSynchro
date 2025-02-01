using BSynchro.RJP.Accounts.Domain.Entities;

namespace BSynchro.RJP.Accounts.Domain.Contracts
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);

        void DeleteAsync(T entity);
        void UpdateAsync(T entity);

        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
