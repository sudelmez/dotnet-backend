using Dapper;
using System.Data;
using TodoApi2.src.Core.Contracts;

namespace TodoApi2.src.Core.Domain.Data
{
    public class ApplicationDbContext : IApplicationDbContext
    {
        private readonly IDbConnection _db;
        public ApplicationDbContext(IDbConnection db)
        {
            _db = db;
        }
        public async Task<IEnumerable<ProductEntity>> GetProducts()
        {
            var sql = "SELECT * FROM Product";
            var res = await _db.QueryAsync<ProductEntity>(sql);
            return res;
        }
        public async Task<IEnumerable<ProductEntity>?> GetProductsById(string id)
        {
            var sql = "SELECT * FROM Product WHERE UserId = @UserId";
            var res = await _db.QueryAsync<ProductEntity>(sql, new { UserId = id });
            if (res == null)
            {
                return null;
            }
            return res;
        }
        public async Task<List<ProductEntity>?> AddProducts(List<ProductEntity> product)
        {
            var sql = "SELECT ProductNo, PolicyNo, Premium, Plate,Insured FROM Product WHERE UserId = @UserId";
            var res = await _db.QueryAsync<ProductDto>(sql);
            return (List<ProductEntity>?)res;
        }
    }
}