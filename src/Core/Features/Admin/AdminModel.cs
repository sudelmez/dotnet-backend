namespace TodoApi2.Features.User;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;
public class AdminModel
{
    [BsonRepresentation(BsonType.ObjectId)]
    public ObjectId Id { get; set; }
    public string UId { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public string RoleId { get; set; }

    public BsonDocument ToBson(AdminModel user)
    {
        BsonDocument BUser = new BsonDocument{
        { "UId", Guid.NewGuid().ToString() },
        { "Name", user.Name },
        { "LastName", user.LastName },
        { "Password", user.Password },
        { "Email", user.Email },
        { "RoleId", user.RoleId },
    };
        return BUser;
    }
    public AdminModel FromBson(BsonDocument document)
    {
        var user = BsonSerializer.Deserialize<AdminModel>(document);
        return user;
    }
}

