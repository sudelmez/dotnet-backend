namespace TodoApi2.src.Core.Domain.Contracts
{
    public interface IProductRepository
    {
        Task<List<ProductEntity>> GetProducts();
        Task<List<ProductEntity>?> GetProductsById(string id);
        Task<List<AddProductEntity>?> AddProducts(List<AddProductEntity> product);
    }
}