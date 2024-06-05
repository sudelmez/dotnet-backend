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
    private IEnumerable<Users> GetUsersFromJsonFile()
    {
        var filePath = Path.Combine("./././users.json");

        var jsonString = System.IO.File.ReadAllText(filePath);
        var users = JsonSerializer.Deserialize<List<Users>>(jsonString);
        return users;
    }

    [HttpPost("update")]
    public IActionResult Update([FromBody] List<Users> users)
    {
        var filePath = Path.Combine("././users.json");
        string newList = JsonSerializer.Serialize(users);
        System.IO.File.WriteAllText(filePath, newList);

        return Ok();

    }

    [HttpPost("delete")]
    public IActionResult Delete(Users request)
    {
        var users = GetUsersFromJsonFile();
        var newUsers = users.Where(u => u.Id != request.Id).ToList();
        Update(newUsers);
        return Ok();
    }

    [HttpGet(Name = "get")]
    public IEnumerable<Users> Get()
    {
        var users = GetUsersFromJsonFile();
        return users;
    }
}