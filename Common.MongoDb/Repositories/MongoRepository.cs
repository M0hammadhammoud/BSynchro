using Common.MongoDb.Contracts;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace Common.MongoDb.Repositories
{
    public class MongoRepository<TEntity> : IMongoRepository<TEntity>
    {
        private readonly IMongoCollection<TEntity> _collection;

        public MongoRepository(IMongoCollection<TEntity> collection)
        {
            _collection = collection;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public async Task InsertAsync(TEntity entity)
        {
            await _collection.InsertOneAsync(entity);
        }

        public async Task InsertAsync(IEnumerable<TEntity> entities)
        {
            await _collection.InsertManyAsync(entities);
        }

        public async Task DeleteAync(Expression<Func<TEntity, bool>> expression)
        {
            await _collection.DeleteManyAsync(expression);
        }

        public async Task UpdateAsync(Expression<Func<TEntity, bool>> expression, UpdateDefinition<TEntity> update)
        {
            await _collection.UpdateManyAsync(expression, update);
        }

        public async Task<IEnumerable<TEntity>> GetByConditionAsync(Expression<Func<TEntity, bool>> filter,
                                                                    Expression<Func<TEntity, object>>? orderBy,
                                                                    int? count)
        {
            if (count != null)
            {
                if (orderBy != null)
                {
                    return await _collection.Find(filter).SortBy(orderBy).Limit(count).ToListAsync();
                }
                return await _collection.Find(filter).Limit(count).ToListAsync();
            }
            if (orderBy != null)
            {
                return await _collection.Find(filter).SortBy(orderBy).ToListAsync();
            }
            return await _collection.Find(filter).ToListAsync();
        }

        public async Task<long> GetCountByConditionAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await _collection.CountDocumentsAsync(filter);
        }

        public async Task<decimal> GetPropertySumByConditionAsync(Expression<Func<TEntity, bool>> filter, string property)
        {
            var sumResult = await _collection.Aggregate()
                                             .Match(filter)
                                             .Group(new BsonDocument
                                             {
                                                 { "_id", BsonNull.Value },
                                                 { "total", new BsonDocument("$sum", $"${property}") }
                                             })
                                             .FirstOrDefaultAsync();

            return sumResult?["total"].AsDecimal ?? 0;
        }
    }
}
