namespace TodoApi2.Features.User;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
public class User
{
    [BsonRepresentation(BsonType.ObjectId)]
    // public ObjectId _id { get; set; }
    public ObjectId Id { get; set; }
    public string UId { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }
    public string Password { get; set; }
    public string LastName { get; set; }
    public string Token { get; set; }
    public string Client { get; set; }
    public List<string> AuthorizedProducts { get; set; }
    public DateTime CreatedDate { get; set; }
}

