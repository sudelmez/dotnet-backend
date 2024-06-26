using MongoDB.Bson;

namespace TodoApi2.src.Core.Contracts;

public interface IAccessibilityService
{
    Task<BsonDocument>? GetRole(string RoleId);
    Task<BsonDocument>? GetRoleAccessibility(string RoleId);
}