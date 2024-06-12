using MongoDB.Bson;
using TodoApi2.Features.User;
namespace TodoApi2.Core.Contracts;

public interface IMongoDBService
{
    Task<BsonDocument>? Add(BsonDocument document, bool isLog);
    BsonDocument? DelById(string UId);
    BsonDocument GetById(string UId);
    List<BsonDocument> Get();
    User? Auth(string email, string password);
    BsonDocument GetRole(string RoleId);
    BsonDocument GetAccessibility(string RoleId);
}