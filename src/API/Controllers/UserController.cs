using Microsoft.AspNetCore.Mvc;
using TodoApi2.src.Core.Contracts;
using TodoApi2.Features.User;
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
    public static string GenerateRandomPolicyNumber(int length)
    {
        var rndDigits = new System.Text.StringBuilder().Insert(0, "0123456789", length).ToString().ToCharArray();
        return string.Join("", rndDigits.OrderBy(o => Guid.NewGuid()).Take(length));
    }
    private List<ProductEntity> createProduct(BsonDocument user)
    {
        var u = _user.FromBson(user);
        var productList = new List<ProductEntity>();
        foreach (var authorizedProduct in u.AuthorizedProducts)
        {
            Random random = new Random();
            float premium = (float)(random.NextDouble() * 1000);
            string policy = GenerateRandomPolicyNumber(15);
            // string plate = GenerateRandomPlate(); TODO
            string plate = "34ABC345";
            var product = new ProductEntity(
                u.UId,
                authorizedProduct,
                policy,
                premium,
                // insured
                "John Doe",
                plate,
                u.CreatedDate
            );
            productList.Add(product);
        }
        return productList;
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
        var res = await _userService.Delete(request.UId);
        if (res != null)
        {
            return Ok(res);
        }
        else { return NotFound(); }
    }

    [HttpPost("add")]
    public async Task<IActionResult> Add(User request)
    {
        var user = _user.ToBson(request);
        await _userService.Add(user);
        createProduct(user);
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
        var user = _userService.GetUser(UId);
        User receivedUser = await _user.FromBson(user);
        return receivedUser;
    }
}