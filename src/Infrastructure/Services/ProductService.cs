using AutoMapper;
using TodoApi2.src.Core.Contracts;
using TodoApi2.src.Core.Domain.Contracts;

namespace TodoApi2.src.Infrastructure.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productReppository;
        private readonly IMapper _mapper;
        public ProductService(IProductRepository productReppository, IMapper mapper)
        {
            _productReppository = productReppository;
            _mapper = mapper;
        }
        public async Task<List<ProductDto>> GetProducts()
        {
            var products = await _productReppository.GetProducts();
            var mappedList = _mapper.Map<List<ProductEntity>, List<ProductDto>>(products);
            return mappedList;
        }
        public async Task<List<ProductDto>> GetProductsById(string id)
        {
            var products = await _productReppository.GetProductsById(id);
            var mappedList = _mapper.Map<List<ProductEntity>, List<ProductDto>>(products);
            return mappedList;
        }
    }
}