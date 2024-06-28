using Microsoft.AspNetCore.Mvc;
using TodoApi2.src.Core.Contracts;

namespace TodoApi2.src.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(ILogger<ProductsController> logger, IProductService productService)
        {
            _productService = productService;
            _logger = logger;
        }

        [HttpGet("get")]
        public async Task<IEnumerable<ProductDto>> Get()
        {
            var products = await _productService.GetProducts();
            return products;
        }

        [HttpGet("getById")]
        public async Task<IEnumerable<ProductDto>> GetById(string uId)
        {
            var products = await _productService.GetProductsById(uId);
            return products;
        }
    }
}
