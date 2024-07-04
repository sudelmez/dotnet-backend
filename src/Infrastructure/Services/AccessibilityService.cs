using MongoDB.Bson;
using TodoApi2.src.Core.Contracts;

namespace TodoApi2.src.Infrastructure.Services
{
    public class AccessibilityService : IAccessibilityService
    {
        private IMongoDBService _mongoDbService;
        public AccessibilityService(IMongoDBService mongoDbService)
        {
            _mongoDbService = mongoDbService;
        }
        public async Task<BsonDocument>? GetRole(string RoleId)
        {
            var role = await _mongoDbService.GetRole(RoleId);
            return role;
        }
        public async Task<BsonDocument>? GetRoleAccessibility(string RoleId)
        {
            var accessibility = await _mongoDbService.GetAccessibility(RoleId);
            return accessibility;
        }

    }

}