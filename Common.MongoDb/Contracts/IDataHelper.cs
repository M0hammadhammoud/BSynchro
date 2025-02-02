using Common.MongoDb.Models;
using System.Linq.Expressions;

namespace Common.MongoDb.Contracts
{
    public interface IDataHelper
    {
        Task<bool> AddNewRecord<T>(T entity, string collectionName);
        Task<bool> AddBulkRecords<T>(IEnumerable<T> entities, string collectionName);
        Task<IEnumerable<T>> GetRecordsByCondition<T>(Expression<Func<T, bool>> filter,
                                                      Expression<Func<T, object>>? orderBy,
                                                      int? count,
                                                      string collectionName);
        Task<bool> UpdateRecords<T>(Expression<Func<T, bool>> filter, List<UpdateDefinitionValue> updates, string collectionName);
        Task<long> GetCountByCondition<T>(Expression<Func<T, bool>> filter, string collectionName);
        Task<decimal> GetPropertySumByCondition<T>(Expression<Func<T, bool>> filter, string property, string collectionName);
    }
}
