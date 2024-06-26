using Microsoft.AspNetCore.Mvc;
using TodoApi2.src.Core.Contracts;
namespace TodoApi2.src.API.Controllers;

[Route("[controller]")]
[ApiController]
public class AccessibilityController : ControllerBase
{
    private IAccessibilityService _accessibilityService;

    public AccessibilityController(IAccessibilityService accessibilityService)
    {
        _accessibilityService = accessibilityService;
    }
    Role _role = new Role();
    Accessibility _acc = new Accessibility();

    [HttpGet("getRole")]
    public async Task<Role> GetRole(string RoleId)
    {
        var role = await _accessibilityService.GetRole(RoleId);
        var recRole = _role.FromBson(role);
        return recRole;
    }
    [HttpGet("getRoleAccessibility")]
    public async Task<Accessibility> GetRoleAccessibility(string RoleId)
    {
        var acc = await _accessibilityService.GetRoleAccessibility(RoleId);
        Accessibility accessibility = _acc.FromBson(acc);
        return accessibility;
    }
}