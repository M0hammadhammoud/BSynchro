using BSynchro.RJP.Transactions.Domain.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BSynchro.RJP.Transactions.Domain.Documents
{
    public class Transaction : BaseDocument
    {
        [BsonRepresentation(BsonType.String)]
        public Guid AccountId { get; set; }
        public decimal Amount { get; set; }
        public TransactionTypeEnum TransactionType { get; set; }
        public DateTime TransactedOn { get; set; }
    }
}
