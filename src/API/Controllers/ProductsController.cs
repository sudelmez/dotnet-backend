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

        public static string GenerateRandomPolicyNumber(int length)
        {
            var rndDigits = new System.Text.StringBuilder().Insert(0, "0123456789", length).ToString().ToCharArray();
            Random random = new Random();
            float premium = (float)(random.NextDouble() * 1000);
            return string.Join("", rndDigits.OrderBy(o => Guid.NewGuid()).Take(length));
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

        [HttpPost("add")]
        public async Task<IEnumerable<AddProductDto>> Add(List<AddProductDto> product)
        {
            var products = await _productService.AddProduct(product);
            return products;
        }
    }
}
