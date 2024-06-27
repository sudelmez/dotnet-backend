using TodoApi2.src.Core.Domain.Repository;

namespace TodoApi2.src.Infrastructure.Services
{
    public class ProductService
    {
        private readonly ProductRepository _productReppository;
        public ProductService(ProductRepository productReppository)
        {
            _productReppository = productReppository;
        }
        //TODO service fonksiyonları implement edilecek.
    }
}