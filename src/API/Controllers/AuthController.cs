using Microsoft.AspNetCore.Mvc;
using TodoApi2.Core.Contracts;
using TodoApi2.Data;
using TodoApi2.Features.Login;
namespace TodoApi2.API.Controllers;
[Route("[controller]")]
[ApiController]

public class AuthController : ControllerBase
{
    private readonly ILogger<AuthController> _logger;
    private IMongoDBService _mongoDbService;
    public AuthController(ILogger<AuthController> logger, IMongoDBService mongoDbService)
    {
        _logger = logger;
        _mongoDbService = mongoDbService;
    }
    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        _logger.LogInformation("Received login request: Email = {Email}, Password = {Password}", request.Email, request.Password);
        var user = _mongoDbService.Auth(request.Email, request.Password);
        _logger.LogInformation("req: {request}", request.Email);
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