using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;

public class Role
{
    [BsonRepresentation(BsonType.ObjectId)]
    public ObjectId Id { get; set; }
    public string RoleId { get; set; }
    public string RoleName { get; set; }
    public BsonDocument ToBson(Role role)
    {
        BsonDocument BRole = new BsonDocument{
        { "RoleId", role.RoleId},
        { "RoleName", role.RoleName }
    };
        return BRole;
    }
    public Role FromBson(BsonDocument document)
    {
        var role = BsonSerializer.Deserialize<Role>(document);
        return role;
    }
}