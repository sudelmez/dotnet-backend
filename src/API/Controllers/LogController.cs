using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using TodoApi2.Data;
namespace TodoApi2.Controllers;

[Route("[controller]")]
[ApiController]
public class LogController : ControllerBase
{
    private readonly ILogger<LogController> _logger;
    private MongoDbService _mongoDbService;
    public LogController(ILogger<LogController> logger, MongoDbService mongoDbService)
    {
        _logger = logger;
        _mongoDbService = mongoDbService;
    }
    public static string GetMessage(Status statu)
    {
        var Message = "";
        if (statu == Status.Success) { Message = "Successful"; }
        else if (statu == Status.Wrong) { Message = "Wrong mail or password"; }
        else if (statu == Status.NotFound) { Message = "Invalid user"; }
        return Message;
    }
    public enum Status
    {
        Success,
        Wrong,
        NotFound
    }
    public void SendLog(Status statu, string userName)
    {
        var logModel = new Log
        {
            UserName = userName,
            CreatedDate = DateTime.Now,
            Message = GetMessage(statu),
            IpAdress = "" //TODO ip alınacak
        };
        LogData(logModel);
    }

    [HttpPost("log")]
    public ActionResult LogData(Log log)
    {
        try
        {
            BsonDocument logInfo = new BsonDocument{
        { "UserName", log.UserName },
        { "IpAdress",log.IpAdress },
        { "DateTime", log.CreatedDate },
        { "Log", log.Message }
    }; _mongoDbService.Add(logInfo, true);
            return Ok();
        }
        catch (System.Exception)
        {
            throw;
        }
    }
}
