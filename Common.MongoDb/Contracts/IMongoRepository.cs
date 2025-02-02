using MongoDB.Driver;
using System.Linq.Expressions;


namespace Common.MongoDb.Contracts
{
    public interface IMongoRepository<TEntity>
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task InsertAsync(TEntity entity);
        Task InsertAsync(IEnumerable<TEntity> entities);
        Task DeleteAync(Expression<Func<TEntity, bool>> expression);
        Task UpdateAsync(Expression<Func<TEntity, bool>> expression, UpdateDefinition<TEntity> update);
        Task<IEnumerable<TEntity>> GetByConditionAsync(Expression<Func<TEntity, bool>> filter,
                                                       Expression<Func<TEntity, object>>? orderBy,
                                                       int? count);
        Task<long> GetCountByConditionAsync(Expression<Func<TEntity, bool>> filter);
        Task<decimal> GetPropertySumByConditionAsync(Expression<Func<TEntity, bool>> filter, string property);
    }
}
