using Dapper;
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
        public async Task<List<ProductEntity>> GetProducts()
        {
            var res = await _db.GetProducts();
            return res.AsList();
        }
        public async Task<List<ProductEntity>?> GetProductsById(string id)
        {
            var res = await _db.GetProductsById(id);
            return res.AsList();
        }
        public async Task<List<ProductEntity>?> AddProducts(List<ProductEntity> product)
        {
            var res = await _db.AddProducts(product);
            return res;
        }
    }

}