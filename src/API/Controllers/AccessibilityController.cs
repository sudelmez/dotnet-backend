using Microsoft.AspNetCore.Mvc;
using TodoApi2.Core.Contracts;
namespace TodoApi2.API.Controllers;

[Route("[controller]")]
[ApiController]
public class AccessibilityController : ControllerBase
{
    private IMongoDBService _mongoDbService;
    Role _role = new Role();
    Accessibility _acc = new Accessibility();
    public AccessibilityController(IMongoDBService mongoDbService)
    {
        _mongoDbService = mongoDbService;
    }
    [HttpGet("getRole")]
    public ActionResult<Role> GetRole(string RoleId)
    {
        var role = _mongoDbService.GetRole(RoleId);
        var recRole = _role.FromBson(role);
        return recRole;
    }
    [HttpGet("getRoleAccessibility")]
    public ActionResult<Accessibility> GetRoleAccessibility(string RoleId)
    {
        var acc = _mongoDbService.GetAccessibility(RoleId);
        Accessibility accessibility = _acc.FromBson(acc);
        return accessibility;
    }
}