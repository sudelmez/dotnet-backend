namespace TodoApi2.src.Core.Domain.Contracts
{
    public interface IProductRepository
    {
        Task<IEnumerable<ProductEntity>> GetProducts();
        Task<List<ProductEntity>?> GetProductsById(string id);
        Task<List<ProductEntity>?> AddProducts(string id);

    }

}