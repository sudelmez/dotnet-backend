using MongoDB.Bson;
using MongoDB.Bson.Serialization;

namespace TodoApi2.Features.Log;

public class Log
{
    public string UserName { get; set; }
    public DateTime CreatedDate { get; set; }
    public string Message { get; set; }
    public string IpAdress { get; set; }
    public string UserAgent { get; set; }
    public BsonDocument ToBson(Log log)
    {
        BsonDocument BLog = new BsonDocument{
        { "UserName", log.UserName },
        { "IpAdress",log.IpAdress },
        { "DateTime", log.CreatedDate },
        { "Log", log.Message },
        {"UserAgent", log.UserAgent}
    };
        return BLog;
    }
    public Log FromBson(BsonDocument document)
    {
        var user = BsonSerializer.Deserialize<Log>(document);
        return user;
    }
}