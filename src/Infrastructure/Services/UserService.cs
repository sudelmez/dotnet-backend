using MongoDB.Bson;
using TodoApi2.Core.Contracts;

namespace TodoApi2.src.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private IMongoDBService _mongoDbService;

        public UserService(IMongoDBService mongoDbService)
        {
            _mongoDbService = mongoDbService;
        }
        public async Task<List<BsonDocument>> Get()
        {
            return await _mongoDbService.Get();
        }
        public async Task<BsonDocument> Add(BsonDocument doc)
        {
            return await _mongoDbService.Add(doc, false);
        }
        public async Task<BsonDocument> Delete(string uId)
        {
            return await _mongoDbService.DelById(uId);
        }
        public async Task<BsonDocument> Update(BsonDocument doc, string uId)
        {
            return await _mongoDbService.Update(doc, uId);
        }
        public async Task<BsonDocument> GetUser(string uId)
        {
            return await _mongoDbService.GetById(uId);
        }
    }
}