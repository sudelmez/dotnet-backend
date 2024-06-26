using MongoDB.Bson;
using TodoApi2.Features.User;
namespace TodoApi2.Core.Contracts;

public interface IMongoDBService
{
    Task<BsonDocument>? Add(BsonDocument document, bool isLog);
    Task<BsonDocument>? DelById(string UId);
    Task<BsonDocument>? GetById(string UId);
    Task<List<BsonDocument>> Get();
    Task<AdminModel>? Auth(string email, string password);
    Task<BsonDocument>? GetRole(string RoleId);
    Task<BsonDocument>? GetAccessibility(string RoleId);
    Task<BsonDocument>? Update(BsonDocument document, string uId);
}