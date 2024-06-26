using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TodoApi2.src.Core.Domain.Entities
{
    public class Accessibility
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }
        public bool AddUser { get; set; }
        public bool DelUser { get; set; }
        public bool SeeUserList { get; set; }
        public bool SeeUserDetail { get; set; }
        public string RoleId { get; set; }

    }
}