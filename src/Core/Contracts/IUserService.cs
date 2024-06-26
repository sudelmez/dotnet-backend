using MongoDB.Bson;
namespace TodoApi2.src.Core.Contracts;

public interface IUserService
{
    Task<List<BsonDocument>> Get();
    Task<BsonDocument> Add(BsonDocument doc);
    Task<BsonDocument> Delete(string uId);
    Task<BsonDocument> Update(BsonDocument doc, string uId);
    Task<BsonDocument> GetUser(string uId);
}