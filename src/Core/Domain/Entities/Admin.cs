using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TodoApi2.src.Core.Domain.Entities
{
    public class Admin
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }
        public string UId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string RoleId { get; set; }
    }
}