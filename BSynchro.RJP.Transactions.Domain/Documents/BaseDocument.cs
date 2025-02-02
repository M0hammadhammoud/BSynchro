using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace BSynchro.RJP.Transactions.Domain.Documents
{
    public abstract class BaseDocument
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public Guid Id { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    }
}
