using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson.Serialization;
using System.Text.Json;
using TodoApi2.Data;
using TodoApi2.Features.Login;
using TodoApi2.Features.User;
namespace TodoApi2.Controllers;
[Route("[controller]")]
[ApiController]

public class AuthController : ControllerBase
{
    private readonly ILogger<AuthController> _logger;
    private MongoDbService _mongoDbService;

    public AuthController(ILogger<AuthController> logger, MongoDbService mongoDbService)
    {
        _logger = logger;
        _mongoDbService = mongoDbService;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        _logger.LogInformation("Received login request: Email = {Email}, Password = {Password}", request.Email, request.Password);
        var users = _mongoDbService.Get();
        //TODO userListController içerisinde bu işlemi yapan bir fonksiyon var. ve burası nasıl daha iyi yapılabilir?
        var userList = new List<User>();
        foreach (var doc in users)
        {
            var u = BsonSerializer.Deserialize<User>(doc);
            userList.Add(u);
        }
        var user = userList.FirstOrDefault(u => u.Email == request.Email && u.Password == request.Password);
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
                user.Token,
                user.Client,
                user.AuthorizedProducts,
                user.CreatedDate
            },
        });

    }
}