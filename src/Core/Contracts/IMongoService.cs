using System.Net;
using MongoDB.Bson;
using TodoApi2.Features.User;
namespace TodoApi2.Core.Contracts;

public interface IMongoDBService
{
    Task<BsonDocument>? Add(BsonDocument document, bool isLog);
    IPAddress? GetRemoteHostIpAddressUsingRemoteIpAddress();
    BsonDocument? DelById(string UId);
    BsonDocument GetById(string UId);
    List<BsonDocument> Get();
    User? Auth(string email, string password);
}