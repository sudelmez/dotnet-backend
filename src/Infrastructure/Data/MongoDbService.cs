namespace TodoApi2.Data;
using System;
using System.Net;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using TodoApi2.Core.Contracts;
using TodoApi2.Features.Log;
using TodoApi2.Features.User;

public class MongoDbService : IMongoDBService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IMongoCollection<BsonDocument> _collection;
    private readonly IMongoCollection<BsonDocument> _collectionLog;
    public MongoDbService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
        var connectionString = "mongodb://localhost:27017";
        var client = new MongoClient(connectionString);
        var database = client.GetDatabase("portal");
        _collection = database.GetCollection<BsonDocument>("users");
        _collectionLog = database.GetCollection<BsonDocument>("authorization");
    }
    public string GetMessage(Status statu)
    {
        var Message = "";
        if (statu == Status.Success) { Message = "Successful"; }
        else if (statu == Status.Wrong) { Message = "Wrong mail or password"; }
        else if (statu == Status.NotFound) { Message = "Invalid user"; }
        return Message;
    }
    public enum Status
    {
        Success,
        Wrong,
        NotFound
    }
    public IPAddress? GetRemoteHostIpAddressUsingRemoteIpAddress()
    {
        return _httpContextAccessor?.HttpContext?.Connection?.RemoteIpAddress?.MapToIPv4();
    }
    public async void SendLog(string userName, Status statu)
    {

        var remoteIpAddress = GetRemoteHostIpAddressUsingRemoteIpAddress();

        var logModel = new Log
        {
            UserName = userName,
            CreatedDate = DateTime.Now,
            Message = GetMessage(statu),
            IpAdress = remoteIpAddress?.ToString() ?? ""
        };
        try
        {
            BsonDocument logInfo = new BsonDocument{
        { "UserName", logModel.UserName },
        { "IpAdress",logModel.IpAdress },
        { "DateTime", logModel.CreatedDate },
        { "Log", logModel.Message }};
            await Add(logInfo, true);
            return;
        }
        catch (System.Exception)
        { throw; }
    }

    public List<BsonDocument> Get()
    {
        List<BsonDocument> documents = _collection.Find(new BsonDocument()).ToList();
        foreach (var document in documents)
        {
            Console.WriteLine(document);
        }
        return documents;
    }

    public async Task<BsonDocument>? Add(BsonDocument document, bool isLog)
    {
        if (isLog)
        {
            await _collectionLog.InsertOneAsync(document);
        }
        else { await _collection.InsertOneAsync(document); }
        return null;
    }

    public BsonDocument GetById(string UId)
    {
        var filter = Builders<BsonDocument>.Filter.Eq("UId", UId);
        return _collection.Find(filter).FirstOrDefault();
    }

    public BsonDocument? DelById(string UId)
    {
        var filter = Builders<BsonDocument>.Filter.Eq("UId", UId);
        _collection.FindOneAndDelete(filter);
        return null;
    }

    public User? Auth(string email, string password)
    {
        var filter = Builders<BsonDocument>.Filter.Eq("Email", email);
        var resUser = _collection.Find(filter).FirstOrDefault();
        if (resUser == null)
        {
            SendLog(email, Status.NotFound);
            return null;
        }

        var user = BsonSerializer.Deserialize<User>(_collection.Find(filter).FirstOrDefault());
        if (user.Password != password)
        {
            SendLog(email, Status.Wrong);
        }
        else if (user.Password == password)
        {
            SendLog(email, Status.Success);
        }
        return user.Password == password ? user : null;
    }
}

