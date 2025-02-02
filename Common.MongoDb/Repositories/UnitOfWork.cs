using Common.MongoDb.Contexts;

namespace Common.MongoDb.Repositories
{
    public class UnitOfWork
    {
        private readonly MongoDbContext _context;

        public UnitOfWork(MongoDbContext context)
        {
            _context = context;
        }

        public MongoRepository<TEntity> GetRepository<TEntity>(string collectionName)
        {
            return new MongoRepository<TEntity>(_context.GetCollection<TEntity>(collectionName));
        }

        public async Task CommitAsync()
        {
        }
    }
}
