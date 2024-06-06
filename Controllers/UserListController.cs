using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
namespace TodoApi2.Controllers;
[Route("[controller]")]
[ApiController]

public class UserListController : ControllerBase
{
    private readonly ILogger<UserListController> _logger;
    public UserListController(ILogger<UserListController> logger)
    {
        _logger = logger;
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

    [HttpGet(Name = "get")]
    public IEnumerable<User> Get()
    {
        var users = GetUsersFromJsonFile();
        return users;
    }
}