namespace TodoApi2.Data;
using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using TodoApi2.Core.Contracts;
using TodoApi2.Features.User;

public class MongoDbService : IMongoDBService
{
    private readonly IMongoCollection<BsonDocument> _collection;
    private readonly IMongoCollection<BsonDocument> _collectionLog;
    private readonly IMongoCollection<BsonDocument> _collectionRoles;
    private readonly IMongoCollection<BsonDocument> _collectionAcc;
    private readonly IMongoCollection<BsonDocument> _collectionAdmins;
    public MongoDbService()
    {
        var connectionString = "mongodb://localhost:27017";
        var client = new MongoClient(connectionString);
        var database = client.GetDatabase("portal");
        _collection = database.GetCollection<BsonDocument>("users");
        _collectionAdmins = database.GetCollection<BsonDocument>("admins");
        _collectionLog = database.GetCollection<BsonDocument>("authorization");
        _collectionRoles = database.GetCollection<BsonDocument>("roles");
        _collectionAcc = database.GetCollection<BsonDocument>("accessibility");
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
    public BsonDocument GetRole(string RoleId)
    {
        var filter = Builders<BsonDocument>.Filter.Eq("RoleId", RoleId);
        var recRoles = _collectionRoles.Find(filter).FirstOrDefault();
        return recRoles;
    }

    public BsonDocument GetAccessibility(string RoleId)
    {
        var filter = Builders<BsonDocument>.Filter.Eq("RoleId", RoleId);
        BsonDocument documents = _collectionAcc.Find(filter).FirstOrDefault();
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

    public AdminModel? Auth(string email, string password)
    {
        var filter = Builders<BsonDocument>.Filter.Eq("Email", email);
        var resUser = _collectionAdmins.Find(filter).FirstOrDefault();
        if (resUser == null)
        {
            return null;
        }
        var user = BsonSerializer.Deserialize<AdminModel>(resUser);
        return user;
    }
}

