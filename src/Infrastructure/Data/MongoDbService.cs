namespace TodoApi2.src.Infrastructure.Data;
using System;
using MongoDB.Bson;
using MongoDB.Driver;
using TodoApi2.src.Core.Contracts;

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
    public async Task<List<BsonDocument>> Get()
    {
        List<BsonDocument> documents = await _collection.Find(new BsonDocument()).ToListAsync();
        foreach (var document in documents)
        {
            Console.WriteLine(document);
        }
        return documents;
    }
    public async Task<BsonDocument>? GetRole(string RoleId)
    {
        var filter = Builders<BsonDocument>.Filter.Eq("RoleId", RoleId);
        var recRoles = await _collectionRoles.Find(filter).FirstOrDefaultAsync();
        return recRoles;
    }

    public async Task<BsonDocument>? GetAccessibility(string RoleId)
    {
        var filter = Builders<BsonDocument>.Filter.Eq("RoleId", RoleId);
        BsonDocument documents = await _collectionAcc.Find(filter).FirstOrDefaultAsync();
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

    public async Task<BsonDocument>? GetById(string UId)
    {
        var filter = Builders<BsonDocument>.Filter.Eq("UId", UId);
        var user = await _collection.Find(filter).FirstOrDefaultAsync();
        return user;
    }

    public async Task<BsonDocument?> DelById(string UId)
    {
        var filter = Builders<BsonDocument>.Filter.Eq("UId", UId);
        var result = await _collection.FindOneAndDeleteAsync(filter);
        return result;
    }

    public async Task<BsonDocument>? Update(BsonDocument document, string uId)
    {
        var filter = Builders<BsonDocument>.Filter.Eq("UId", uId);
        var updateDefinition = new BsonDocument { { "$set", document } };
        await _collection.UpdateOneAsync(filter, updateDefinition);
        return document;
    }

    public async Task<BsonDocument>? Auth(string email, string password)
    {
        var filter = Builders<BsonDocument>.Filter.Eq("Email", email);
        var resUser = await _collectionAdmins.Find(filter).FirstOrDefaultAsync();
        if (resUser == null)
        {
            return null;
        }
        return resUser;
    }
}

