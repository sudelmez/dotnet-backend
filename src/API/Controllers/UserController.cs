using Microsoft.AspNetCore.Mvc;
using TodoApi2.Core.Contracts;
using TodoApi2.Features.User;
namespace TodoApi2.API.Controllers;
[Route("[controller]")]
[ApiController]

public class UserListController : ControllerBase
{
    private readonly ILogger<UserListController> _logger;
    private IUserService _userService;
    User _user = new User();
    public UserListController(ILogger<UserListController> logger, IUserService userService)
    {
        _logger = logger;
        _userService = userService;
    }

    [HttpPost("update")]
    public async Task<IActionResult> Update(User user)
    {
        var userB = _user.ToBson(user);
        await _userService.Update(userB, user.UId);
        return Ok();
    }

    [HttpPost("delete")]
    public async Task<IActionResult> Delete(User request)
    {
        await _userService.Delete(request.UId);
        return Ok();
    }

    [HttpPost("add")]
    public async Task<IActionResult> Add(User request)
    {
        var user = _user.ToBson(request);
        await _userService.Add(user);
        return Ok();
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
        User receivedUser = await _user.FromBson(_userService.GetUser(UId));
        return receivedUser;
    }
}