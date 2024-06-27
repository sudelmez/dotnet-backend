namespace TodoApi2.src.Core.Contracts;

public interface IApplicationDbContext
{
    Task<IEnumerable<ProductDto>> GetProducts();
    Task<List<ProductDto>?> GetProductsById(string id);
}