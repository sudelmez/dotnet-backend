using Microsoft.AspNetCore.Mvc;
using TodoApi2.Core.Contracts;
using TodoApi2.Features.User;
namespace TodoApi2.API.Controllers;
[Route("[controller]")]
[ApiController]

public class UserListController : ControllerBase
{
    //IUserService ile bağla
    private readonly ILogger<UserListController> _logger;
    private IMongoDBService _mongoDbService;
    User _user = new User();
    public UserListController(ILogger<UserListController> logger, IMongoDBService mongoDbService)
    {
        _logger = logger;
        _mongoDbService = mongoDbService;
    }

    [HttpPost("update")]
    public IActionResult Update(User user)
    {
        var userB = _user.ToBson(user);
        _mongoDbService.Update(userB, user.UId);
        return Ok();
    }

    [HttpPost("delete")]
    public IActionResult Delete(User request)
    {
        _mongoDbService.DelById(request.UId);
        return Ok();
    }

    [HttpPost("add")]
    public IActionResult Add(User request)
    {
        var user = _user.ToBson(request);
        _mongoDbService.Add(user, false);
        return Ok();
    }

    [HttpGet("get")]
    public IEnumerable<User> Get()
    {
        var users = _mongoDbService.Get();
        _logger.LogInformation("Received users {users}", users);
        var userList = new List<User>();
        foreach (var doc in users)
        {
            var user = _user.FromBson(doc);
            userList.Add(user);
        }
        userList.Sort((x, y) => DateTime.Compare(x.CreatedDate, y.CreatedDate));
        userList.Reverse();
        return userList;
    }

    [HttpGet("getById")]
    public ActionResult<User> GetById(string UId)
    {
        User receivedUser = _user.FromBson(_mongoDbService.GetById(UId));
        return receivedUser;
    }
}