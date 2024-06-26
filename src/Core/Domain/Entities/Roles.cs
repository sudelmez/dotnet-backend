using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TodoApi2.src.Core.Domain.Entities
{
    public class Role
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }
        public string RoleId { get; set; }
        public string RoleName { get; set; }
    }
}