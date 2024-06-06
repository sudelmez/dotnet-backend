namespace TodoApi2.Data;

using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

public class MongoDbService
{
    // private readonly IMongoDatabase _database;
    private readonly IMongoCollection<BsonDocument> _collection;

    public MongoDbService()
    // public MongoDbService(IConfiguration configuration)
    {
        // var connectionString = configuration.GetConnectionString("MongoDb");
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
        // return _collection.Find(new BsonDocument()).ToList();
    }

    public BsonDocument GetById(string id)
    {
        var objectId = ObjectId.Parse(id);
        var filter = Builders<BsonDocument>.Filter.Eq("_id", objectId);
        return _collection.Find(filter).FirstOrDefault();
    }

    public BsonDocument DelById(string id)
    {
        var objectId = ObjectId.Parse(id);
        var filter = Builders<BsonDocument>.Filter.Eq("_id", objectId);
        return _collection.Find(filter).FirstOrDefault();
    }
}
//------------------------

public class MongoDbContext
{
    private readonly IMongoDatabase _database;
    public MongoDbContext()
    {
        var connectionString = "mongodb://localhost:27017";
        var client = new MongoClient(connectionString);
        var database = client.GetDatabase("portal");
    }
    public IMongoCollection<TEntity> GetCollection<TEntity>()
    {
        return _database.GetCollection<TEntity>(typeof(TEntity).Name.Trim());
    }
    public IMongoDatabase GetDatabase()
    {
        return _database;
    }
}

public class EntityResult
{
    public EntityResult()
    {
        Success = true;
    }
    public bool Success { get; set; }
    public string Message { get; set; }
}

public class GetManyResult<TEntity> : EntityResult where TEntity : class, new()
{
    public IEnumerable<TEntity> Result { get; set; }
}

public class GetOneResult<TEntity> : EntityResult where TEntity : class, new()
{
    public TEntity Entity { get; set; }
}
