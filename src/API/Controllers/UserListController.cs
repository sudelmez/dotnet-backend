using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using System.Text.Json;
using TodoApi2.Data;
using TodoApi2.Features.User;
namespace TodoApi2.Controllers;
[Route("[controller]")]
[ApiController]

public class UserListController : ControllerBase
{
    private readonly ILogger<UserListController> _logger;
    private MongoDbService _mongoDbService;
    public UserListController(ILogger<UserListController> logger, MongoDbService mongoDbService)
    {
        _logger = logger;
        _mongoDbService = mongoDbService;
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
        _mongoDbService.DelById(request.UId);
        return Ok();
    }

    [HttpPost("add")]
    public IActionResult Add(User request)
    {
        BsonDocument user = new BsonDocument{
        { "UId", request.UId },
        { "Name", request.Name },
        { "LastName", request.LastName },
        { "Email", request.Email },
        { "Client", request.Client },
        { "Password", request.Password } ,
        { "CreatedDate", DateTime.Now.ToString() } ,
    };
        _mongoDbService.Add(user);
        return Ok();
    }

    [HttpGet("get")]
    public IEnumerable<User> Get()
    {
        var users = _mongoDbService.Get();
        _logger.LogInformation("Received users {users}", users);
        var userList = new List<User>();
        foreach (var doc in users)
        {
            var user = BsonSerializer.Deserialize<User>(doc);
            userList.Add(user);
        }
        return userList;
    }

    [HttpGet("getById")]
    public ActionResult<User> GetById(string UId)
    {
        User receivedUser = BsonSerializer.Deserialize<User>(_mongoDbService.GetById(UId));
        return receivedUser;
    }
}