namespace TodoApi2.src.Core.Contracts;

public interface IApplicationDbContext
{
    Task<IEnumerable<ProductEntity>> GetProducts();
    Task<IEnumerable<ProductEntity>?> GetProductsById(string id);
    Task<List<AddProductEntity>?> AddProducts(List<AddProductEntity> product);
}