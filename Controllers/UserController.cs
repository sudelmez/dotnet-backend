using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
namespace TodoApi2.Controllers;
[Route("[controller]")]
[ApiController]

public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;
    public UserController(ILogger<UserController> logger)
    {
        _logger = logger;
    }
    private IEnumerable<User> GetUserFromJsonFile()
    {
        var filePath = Path.Combine("./././loginuser.json");
        var jsonString = System.IO.File.ReadAllText(filePath);
        var user = JsonSerializer.Deserialize<List<User>>(jsonString);
        return user;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        _logger.LogInformation("Received login request: Email = {Email}, Password = {Password}", request.Email, request.Password);

        var users = GetUserFromJsonFile();
        var user = users.FirstOrDefault(u => u.Email == request.Email && u.Password == request.Password);
        _logger.LogInformation("Users read from JSON: {Users}", users);

        if (user == null)
        {
            _logger.LogWarning("Invalid login attempt for user: {Email}", request.Email);
            return Unauthorized("Invalid email or password.");
        }
        _logger.LogInformation("User {Email} logged in successfully", user.Email);
        return Ok(new
        {
            user = new
            {
                user.Email,
                user.Name,
                user.LastName,
                user.Token
            },
        });

    }
}