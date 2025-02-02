namespace Common.MongoDb.Models
{
    public class UpdateDefinitionValue
    {
        public required string Name { get; set; }
        public required object Value { get; set; }
    }
}
