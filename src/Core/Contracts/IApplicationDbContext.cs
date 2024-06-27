namespace TodoApi2.src.Core.Contracts;

public interface IApplicationDbContext
{
    Task<IEnumerable<ProductEntity>> GetProducts();
    Task<List<ProductEntity>?> GetProductsById(string id);
    Task<IEnumerable<T>> QueryAsync<T>(string sql);
}