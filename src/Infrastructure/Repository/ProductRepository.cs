using TodoApi2.src.Core.Contracts;
using TodoApi2.src.Core.Domain.Contracts;

namespace TodoApi2.src.Core.Domain.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly IApplicationDbContext _db;
        public ProductRepository(IApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<IEnumerable<ProductEntity>> GetProducts()
        {
            var sql = "SELECT * FROM Product";
            var res = await _db.QueryAsync<ProductEntity>(sql);
            return res;
        }
        public async Task<List<ProductEntity>?> GetProductsById(string id)
        {
            var sql = "SELECT * FROM Product WHERE UserId = '{id}'";
            var res = await _db.QueryAsync<ProductEntity>(sql);
            return (List<ProductEntity>?)res;
        }
        public async Task<List<ProductEntity>?> AddProducts(string id)
        {
            var sql = "SELECT ProductNo, PolicyNo, Premium, Plate,Insured FROM Product WHERE UserId = '{id}'";
            var res = await _db.QueryAsync<ProductEntity>(sql);
            return (List<ProductEntity>?)res;
        }

    }

}