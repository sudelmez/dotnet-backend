using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using System.Text.Json;
using TodoApi2.Data;
using TodoApi2.Serializers;
namespace TodoApi2.Controllers;
[Route("[controller]")]
[ApiController]

public class UserListController : ControllerBase
{
    private readonly ILogger<UserListController> _logger;
    private MongoDbService _mongoDbService;
    private BsonStringNumericSerializer _serializer;
    public UserListController(ILogger<UserListController> logger, MongoDbService mongoDbService, BsonStringNumericSerializer serializer)
    {
        _logger = logger;
        _mongoDbService = mongoDbService;
        _serializer = serializer;
    }
    private IEnumerable<User> GetUsersFromJsonFile()
    {
        var filePath = Path.Combine("./././users.json");

        var jsonString = System.IO.File.ReadAllText(filePath);
        var users = JsonSerializer.Deserialize<List<User>>(jsonString);
        return users;
    }

    [HttpPost("update")]
    public IActionResult Update([FromBody] List<User> users)
    {
        var filePath = Path.Combine("././users.json");
        string newList = JsonSerializer.Serialize(users);
        System.IO.File.WriteAllText(filePath, newList);

        return Ok();

    }

    [HttpPost("delete")]
    public IActionResult Delete(User request)
    {
        var users = GetUsersFromJsonFile();
        var newUsers = users.Where(u => u.Id != request.Id).ToList();
        Update(newUsers);
        return Ok();
    }

    // [HttpGet(Name = "get")]
    // public IEnumerable<User> Get()
    // {
    //     var users = GetUsersFromJsonFile();
    //     return users;
    // }

    [HttpGet(Name = "get")]
    public IEnumerable<User> Get()
    {
        var users = _mongoDbService.Get();
        _logger.LogInformation("Received users {users}", users);
        var userList = new List<User>();
        foreach (var doc in users)
        {
            var user = BsonSerializer.Deserialize<User>(doc);
            // user.Id = doc["_id"].AsObjectId.GetHashCode();
            userList.Add(user);
        }
        return userList;
    }

    // [HttpGet(Name = "getById")]
    // public ActionResult<User> GetById(User user)
    // {
    //     string id = user.Id.ToString();
    //     User receivedUser = BsonSerializer.Deserialize<User>(_mongoDbService.GetById(id));
    //     return receivedUser;
    // }
}