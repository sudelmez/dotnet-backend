using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;

public class Accessibility
{
    [BsonRepresentation(BsonType.ObjectId)]
    public ObjectId Id { get; set; }
    public bool AddUser { get; set; }
    public bool DelUser { get; set; }
    public bool SeeUserList { get; set; }
    public bool SeeUserDetail { get; set; }
    public string RoleId { get; set; }
    public BsonDocument ToBson(Accessibility acc)
    {
        BsonDocument BAcc = new BsonDocument{
        { "AddUser", acc.AddUser},
        { "DelUser", acc.DelUser },
        { "SeeUserList", acc.SeeUserList },
        { "SeeUserDetail", acc.SeeUserDetail },
        { "RoleId", acc.RoleId }
    };
        return BAcc;
    }
    public Accessibility FromBson(BsonDocument document)
    {
        var acc = BsonSerializer.Deserialize<Accessibility>(document);
        return acc;
    }
}
