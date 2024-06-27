// using Dapper;
// using System.Data;
// using TodoApi2.src.Core.Contracts;

// namespace TodoApi2.src.Core.Domain.Data
// {
//     public class ApplicationDbContext : IApplicationDbContext
//     {
//         private readonly IDbConnection _db;
//         public ApplicationDbContext(IDbConnection db)
//         {
//             _db = db;
//         }

//         public async Task<IEnumerable<ProductEntity>> GetProducts()
//         {
//             var sql = "SELECT * FROM Product";
//             // var sql = "SELECT ProductNo, PolicyNo, Premium, Plate, Insured FROM Product";
//             var res = await _db.QueryAsync<ProductEntity>(sql);
//             return res;
//         }

//         public async Task<List<ProductEntity>?> GetProductsById(string id)
//         {
//             var sql = "SELECT * FROM Product WHERE UserId = @UserId";
//             var res = await _db.QueryAsync<ProductDto>(sql, new { UserId = id });
//             return (List<ProductEntity>?)res;
//         }
//         public async Task<List<ProductEntity>?> AddProducts(string id)
//         {
//             var sql = "SELECT ProductNo, PolicyNo, Premium, Plate,Insured FROM Product WHERE UserId = @UserId";
//             var res = await _db.QueryAsync<ProductDto>(sql, new { UserId = id });
//             return (List<ProductEntity>?)res;
//         }
//     }
// }