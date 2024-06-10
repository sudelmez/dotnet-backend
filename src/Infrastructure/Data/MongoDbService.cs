namespace TodoApi2.Data;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using TodoApi2.Controllers;
using TodoApi2.Features.User;

public class MongoDbService
{
    private readonly IMongoCollection<BsonDocument> _collection;
    private readonly IMongoCollection<BsonDocument> _collectionLog;
    private LogController _logController;
    // private LogService _logService;
    public MongoDbService(LogController logController)
    {
        _logController = logController;
        var connectionString = "mongodb://localhost:27017";
        var client = new MongoClient(connectionString);
        var database = client.GetDatabase("portal");
        _collection = database.GetCollection<BsonDocument>("users");
        _collectionLog = database.GetCollection<BsonDocument>("authorization");
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

    public BsonDocument? Add(BsonDocument document, bool isLog)
    {
        if (isLog)
        {
            _collectionLog.InsertOneAsync(document);
        }
        else { _collection.InsertOneAsync(document); }
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
        var user = BsonSerializer.Deserialize<User>(_collection.Find(filter).FirstOrDefault());
        if (user == null)
        {
            _logController.SendLog(LogController.Status.NotFound, null);
        }
        else if (user.Password != password)
        {

            _logController.SendLog(LogController.Status.Wrong, email);
        }
        else if (user.Password == password)
        {
            _logController.SendLog(LogController.Status.Success, email);
        }
        return user.Password == password ? user : null;
    }

}

