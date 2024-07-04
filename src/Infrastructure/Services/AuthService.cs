using MongoDB.Bson.Serialization;
using TodoApi2.src.Core.Contracts;
using TodoApi2.Features.Login;
using TodoApi2.Features.User;

namespace TodoApi2.src.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private IMongoDBService _mongoDbService;
        public AuthService(IMongoDBService mongoDbService)
        {
            _mongoDbService = mongoDbService;
        }
        public async Task<AdminModel?> Auth(LoginRequest request)
        {
            var receivedUser = await _mongoDbService.Auth(request.Email, request.Password);
            var user = BsonSerializer.Deserialize<AdminModel>(receivedUser);
            return user;
        }
    }
}