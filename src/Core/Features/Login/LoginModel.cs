using MongoDB.Bson;
using MongoDB.Bson.Serialization;

namespace TodoApi2.Features.Login;
public class LoginRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
    public BsonDocument ToBson(LoginRequest req)
    {
        BsonDocument Breq = new BsonDocument{
        { "UId", req.Email },
        { "Name", req.Password }
    };
        return Breq;
    }
    public LoginRequest FromBson(BsonDocument document)
    {
        var req = BsonSerializer.Deserialize<LoginRequest>(document);
        return req;
    }
}
