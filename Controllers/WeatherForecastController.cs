using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
namespace TodoApi2.Controllers;
[Route("[controller]")]
[ApiController]
public class WeatherForecastController : ControllerBase
{
    private readonly ILogger<WeatherForecastController> _logger;
    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }
    private IEnumerable<User> GetUserFromJsonFile()
    {
        var filePath = Path.Combine("././loginuser.json");
        var jsonString = System.IO.File.ReadAllText(filePath);
        var user = JsonSerializer.Deserialize<List<User>>(jsonString);
        return user;
    }
    private IEnumerable<Users> GetUsersFromJsonFile()
    {
        var filePath = Path.Combine("././users.json");
        var jsonString = System.IO.File.ReadAllText(filePath);
        var users = JsonSerializer.Deserialize<List<Users>>(jsonString);
        return users;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
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

    [HttpGet(Name = "get")]
    public IEnumerable<Users> Get()
    {

        var users = GetUsersFromJsonFile();
        return users;

    }
}
