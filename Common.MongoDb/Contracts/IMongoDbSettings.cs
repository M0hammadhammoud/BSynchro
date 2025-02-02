namespace Common.MongoDb.Contracts
{
    public interface IMongoDbSettings
    {
        string DatabaseName { get; set; }
        string ConnectionString { get; set; }
        string CollectionPrefix { get; set; }
    }
}
