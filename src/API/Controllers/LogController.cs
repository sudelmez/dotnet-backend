// using Microsoft.AspNetCore.Mvc;
// using MongoDB.Bson;
// using TodoApi2.Core.Contracts;
// using TodoApi2.Features.Log;
// namespace TodoApi2.API.Controllers;
// [Route("[logController]")]
// [ApiController]


// public class LogController : ControllerBase
// {
//     private readonly ILogger<LogController> _logger;
//     private readonly IMongoDBService _mongoDbService;
//     public LogController(ILogger<LogController> logger, IMongoDBService mongoDbService)
//     {
//         _logger = logger;
//         _mongoDbService = mongoDbService;
//     }
//     // public static string GetMessage(Status statu)
//     // {
//     //     var Message = "";
//     //     if (statu == Status.Success) { Message = "Successful"; }
//     //     else if (statu == Status.Wrong) { Message = "Wrong mail or password"; }
//     //     else if (statu == Status.NotFound) { Message = "Invalid user"; }
//     //     return Message;
//     // }
//     // public enum Status
//     // {
//     //     Success,
//     //     Wrong,
//     //     NotFound
//     // }

//     // public void SendLog(string userName, Status statu)
//     // {
//     //     var logModel = new Log
//     //     {
//     //         UserName = userName,
//     //         CreatedDate = DateTime.Now,
//     //         Message = GetMessage(statu),
//     //         IpAdress = "" //TODO ip alınacak
//     //     };
//     //     LogData(logModel);
//     // }

//     [HttpPost("log")]
//     public async Task<IActionResult> LogData(Log log)
//     {
//         try
//         {
//             BsonDocument logInfo = new BsonDocument{
//         { "UserName", log.UserName },
//         { "IpAdress",log.IpAdress },
//         { "DateTime", log.CreatedDate },
//         { "Log", log.Message }
//     };
//             await _mongoDbService.Add(logInfo, true);
//             return Ok();
//         }
//         catch (System.Exception)
//         {

//             throw;
//         }

//     }
// }
