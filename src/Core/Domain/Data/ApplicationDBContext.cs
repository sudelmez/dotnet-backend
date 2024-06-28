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
        public async Task<List<AddProductEntity>?> AddProducts(List<AddProductEntity> products)
        {
            var sql = @"
            INSERT INTO Product (UserId, ProductNo, PolicyNo, Premium, Insured, Plate, CreatedDate, Statu)
            VALUES (@UserId, @ProductNo, @PolicyNo, @Premium, @Insured, @Plate, @CreatedDate, @Statu);
            SELECT CAST(SCOPE_IDENTITY() as int);";

            var insertedProducts = new List<AddProductEntity>();

            foreach (var product in products)
            {
                var insertedId = await _db.ExecuteScalarAsync<int>(sql, new
                {
                    product.UserId,
                    product.ProductNo,
                    product.PolicyNo,
                    product.Premium,
                    product.Insured,
                    product.Plate,
                    product.CreatedDate,
                    product.Statu
                });
                insertedProducts.Add(product);
            }

            return insertedProducts;
        }
    }
}