namespace TodoApi2.Data;
using MongoDB.Bson;
using MongoDB.Driver;
using TodoApi2.Features.User;

public class MongoDbService
{
    private readonly IMongoCollection<BsonDocument> _collection;

    public MongoDbService()
    {
        var connectionString = "mongodb://localhost:27017";
        var client = new MongoClient(connectionString);
        var database = client.GetDatabase("portal");
        _collection = database.GetCollection<BsonDocument>("users");
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

    public BsonDocument? Add(BsonDocument document)
    {
        _collection.InsertOneAsync(document);
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

}

