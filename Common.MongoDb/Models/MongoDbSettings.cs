using Common.MongoDb.Contracts;

namespace Common.MongoDb.Models
{
    public class MongoDbSettings : IMongoDbSettings
    {
        public required string DatabaseName { get; set; }
        public required string ConnectionString { get; set; }
        public required string CollectionPrefix { get; set; }
    }
}
