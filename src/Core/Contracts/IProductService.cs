namespace TodoApi2.src.Core.Contracts
{
    public interface IProductService
    {
        Task<List<ProductDto>> GetProducts();
        Task<List<ProductDto>> GetProductsById(string id);
        Task<List<AddProductDto>> AddProduct(List<AddProductDto> product);
    }

}