using Microsoft.AspNetCore.Mvc;
using TodoApi2.src.Core.Contracts;
using TodoApi2.Features.User;
using System.Reflection.Metadata;
using MongoDB.Bson;
namespace TodoApi2.src.API.Controllers;
[Route("[controller]")]
[ApiController]

public class UserListController : ControllerBase
{
    private readonly ILogger<UserListController> _logger;
    private IUserService _userService;

    public UserListController(ILogger<UserListController> logger, IUserService userService)
    {
        _logger = logger;
        _userService = userService;
    }
    User _user = new User();

    [HttpPost("update")]
    public async Task<IActionResult> Update(User user)
    {
        var userB = new BsonDocument{
        { "UId", user.UId },
        { "Name", user.Name },
        { "LastName", user.LastName },
        { "Client", user.Client },
        { "AuthorizedProducts",new BsonArray(user.AuthorizedProducts)},
        { "CreatedDate", DateTime.Now },
    };
        await _userService.Update(userB, user.UId);
        return Ok();
    }

    [HttpPost("delete")]
    public async Task<IActionResult> Delete(User request)
    {
        var res = await _userService.Delete(request.UId);
        if (res != null)
        {
            return Ok(res);
        }
        else { return NotFound(); }
    }

    [HttpPost("add")]
    public async Task<ActionResult<User>> Add(User request)
    {
        var user = _user.ToBson(request);
        var u = await _userService.Add(user);
        var fromBU = _user.FromBson(u);
        return Ok(fromBU);
    }

    [HttpGet("get")]
    public async Task<IEnumerable<User>> Get()
    {
        var users = await _userService.Get();
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
    public async Task<ActionResult<User>> GetById(string UId)
    {
        var user = await _userService.GetUser(UId);
        User receivedUser = MongoDB.Bson.Serialization.BsonSerializer.Deserialize<User>(user);
        return receivedUser;
    }
}