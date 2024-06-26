using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TodoApi2.src.Core.Domain.Entities
{
    public class User
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }
        public string UId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Client { get; set; }
        public List<string> AuthorizedProducts { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}