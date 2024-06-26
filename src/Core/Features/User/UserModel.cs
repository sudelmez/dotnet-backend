namespace TodoApi2.Features.User;

using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;
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

    public BsonDocument ToBson(User user)
    {
        BsonDocument BUser = new BsonDocument{
        { "UId", Guid.NewGuid().ToString() },
        { "Name", user.Name },
        { "LastName", user.LastName },
        { "Client", user.Client },
        { "AuthorizedProducts",new BsonArray(user.AuthorizedProducts)},
        { "CreatedDate", DateTime.Now },
    };
        return BUser;
    }
    public User FromBson(BsonDocument document)
    {
        var user = BsonSerializer.Deserialize<User>(document);
        return user;
    }

    internal async Task<User> FromBson(Task<BsonDocument>? task)
    {
        throw new NotImplementedException();
    }
}

