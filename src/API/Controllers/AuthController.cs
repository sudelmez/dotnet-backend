using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using TodoApi2.Core.Contracts;
using TodoApi2.Features.Log;
using TodoApi2.Features.Login;
namespace TodoApi2.API.Controllers;
[Route("[controller]")]
[ApiController]

public class AuthController : ControllerBase
{
    private readonly ILogger<AuthController> _logger;
    private IMongoDBService _mongoDbService;
    Log _log = new Log();
    public AuthController(ILogger<AuthController> logger, IMongoDBService mongoDbService)
    {
        _logger = logger;
        _mongoDbService = mongoDbService;
    }
    public enum Status
    {
        Success,
        Wrong,
        NotFound
    }
    private string GetMessage(Status statu)
    {
        var Message = "";
        if (statu == Status.Success) { Message = "Successful"; }
        else if (statu == Status.Wrong) { Message = "Wrong mail or password"; }
        else if (statu == Status.NotFound) { Message = "Invalid user"; }
        return Message;
    }
    private async void SendLog(string userName, Status statu)
    {
        var logModel = new Log
        {
            UserName = userName,
            CreatedDate = DateTime.Now,
            Message = GetMessage(statu),
            IpAdress = HttpContext?.Connection?.RemoteIpAddress?.MapToIPv4().ToString() ?? "",
            UserAgent = HttpContext.Request.Headers.UserAgent
        };
        try
        {
            BsonDocument logInfo = _log.ToBson(logModel);
            await _mongoDbService.Add(logInfo, true);
            return;
        }
        catch (System.Exception)
        { throw; }
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        _logger.LogInformation("Received login request: Email = {Email}, Password = {Password}", request.Email, request.Password);
        var user = _mongoDbService.Auth(request.Email, request.Password);
        _logger.LogInformation("req: {request}", request.Email);
        if (user == null)
        {
            SendLog(request.Email, Status.NotFound);
        }
        else if (user.Email == request.Email && user.Password != request.Password)
        {
            SendLog(request.Email, Status.Wrong);
            _logger.LogWarning("Unmatched login attempt for user: {Email}", request.Email);
            return Unauthorized("Unmatched email or password.");
        }
        else if (user.Email == request.Email && user.Password == request.Password)
        {
            _logger.LogInformation("User {Email} logged in successfully", user.Email);
            SendLog(request.Email, Status.Success);
            return Ok(new
            {
                user = new
                {
                    user.Email,
                    user.Name,
                    user.LastName,
                }
            });
        }
        return null;
    }
}