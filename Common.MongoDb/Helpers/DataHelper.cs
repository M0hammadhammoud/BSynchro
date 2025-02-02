using Common.MongoDb.Contexts;
using Common.MongoDb.Contracts;
using Common.MongoDb.Models;
using Common.MongoDb.Repositories;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace Common.MongoDb.Helpers
{
    public class DataHelper : IDataHelper
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly MongoDbContext _dbContext;
        private readonly IMongoDbSettings _mongoDbSettings;

        public DataHelper(MongoDbContext dbContext,
                          IMongoDbSettings mongoDbSettings)
        {
            _dbContext = dbContext;
            _unitOfWork = new UnitOfWork(_dbContext);
            _mongoDbSettings = mongoDbSettings;
        }

        public async Task<bool> AddNewRecord<T>(T entity, string collectionName)
        {
            var repo = _unitOfWork.GetRepository<T>($"{_mongoDbSettings.CollectionPrefix}{collectionName}");
            await repo.InsertAsync(entity);
            await _unitOfWork.CommitAsync();
            return true;
        }

        public async Task<bool> AddBulkRecords<T>(IEnumerable<T> entities, string collectionName)
        {
            var repo = _unitOfWork.GetRepository<T>($"{_mongoDbSettings.CollectionPrefix}{collectionName}");
            await repo.InsertAsync(entities);
            await _unitOfWork.CommitAsync();
            return true;
        }

        public async Task<IEnumerable<T>> GetRecordsByCondition<T>(Expression<Func<T, bool>> filter,
                                                                   Expression<Func<T, object>>? orderBy,
                                                                   int? count,
                                                                   string collectionName)
        {
            var repo = _unitOfWork.GetRepository<T>($"{_mongoDbSettings.CollectionPrefix}{collectionName}");
            return await repo.GetByConditionAsync(filter, orderBy, count);
        }

        public async Task<bool> UpdateRecords<T>(Expression<Func<T, bool>> filter, List<UpdateDefinitionValue> updates, string collectionName)
        {
            var repo = _unitOfWork.GetRepository<T>($"{_mongoDbSettings.CollectionPrefix}{collectionName}");
            var updateDefinitionBuilder = Builders<T>.Update;
            var updateDefinitions = new List<UpdateDefinition<T>>();
            foreach (var item in updates)
            {
                updateDefinitions.Add(updateDefinitionBuilder.Set(item.Name, item.Value));
            }
            await repo.UpdateAsync(filter, updateDefinitionBuilder.Combine(updateDefinitions));
            await _unitOfWork.CommitAsync();
            return true;
        }

        public async Task<long> GetCountByCondition<T>(Expression<Func<T, bool>> filter, string collectionName)
        {
            var repo = _unitOfWork.GetRepository<T>($"{_mongoDbSettings.CollectionPrefix}{collectionName}");
            return await repo.GetCountByConditionAsync(filter);
        }

        public async Task<decimal> GetPropertySumByCondition<T>(Expression<Func<T, bool>> filter, string property, string collectionName)
        {
            var repo = _unitOfWork.GetRepository<T>($"{_mongoDbSettings.CollectionPrefix}{collectionName}");
            return await repo.GetPropertySumByConditionAsync(filter, property);
        }
    }
}
