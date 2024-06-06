namespace TodoApi2.Data;
using MongoDB.Bson;
using MongoDB.Driver;
using Microsoft.Extensions.Configuration;

public class MongoDbService
{
    private readonly IMongoCollection<BsonDocument> _collection;

    public MongoDbService(IConfiguration configuration)
    {
        // var connectionString = configuration.GetConnectionString("MongoDb");
        var connectionString = "mongodb://localhost:27017";
        var client = new MongoClient(connectionString);
        var database = client.GetDatabase("portal");
        _collection = database.GetCollection<BsonDocument>("users");
    }

    public List<BsonDocument> GetAllUsers()
    {
        return _collection.Find(new BsonDocument()).ToList();
    }

    public BsonDocument GetUserById(int id)
    {
        var filter = Builders<BsonDocument>.Filter.Eq("Id", id);
        return _collection.Find(filter).FirstOrDefault();
    }
}
