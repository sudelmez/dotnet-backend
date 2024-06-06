using TodoApi2.Data;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using MongoDB.Bson;
namespace TodoApi2.Controllers;
[Route("[controller]")]
[ApiController]

public class UserListController : ControllerBase
{
    private readonly MongoDbService _mongoDbService;

    public UserListController(MongoDbService mongoDbService)
    {
        _mongoDbService = mongoDbService;
    }

    private IEnumerable<User> GetUsersFromJsonFile()
    {
        var filePath = Path.Combine("./././users.json");

        var jsonString = System.IO.File.ReadAllText(filePath);
        var users = JsonSerializer.Deserialize<List<User>>(jsonString);
        return users;
    }

    [HttpGet("get")]
    public IActionResult GetAllUsers()
    {
        var users = _mongoDbService.GetAllUsers();
        // var userList = JsonSerializer.Deserialize<List<User>>(users);
        var u = users.ConvertAll(BsonTypeMapper.MapToDotNetValue);
        return Ok(users);
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
    //     return (IEnumerable<User>)users;
    // }
}