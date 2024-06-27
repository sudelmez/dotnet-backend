using Microsoft.AspNetCore.Mvc;
using TodoApi2.src.Core.Contracts;

namespace TodoApi2.src.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IApplicationDbContext _db;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(ILogger<ProductsController> logger, IApplicationDbContext db)
        {
            _db = db;
            _logger = logger;
        }

        [HttpGet("get")]
        public async Task<IEnumerable<ProductDto>> Get()
        {
            var products = await _db.GetProducts();
            return products;
        }

        [HttpGet("getById")]
        public async Task<List<ProductDto>> GetById(string uId)
        {
            var products = await _db.GetProductsById(uId);
            return products;
        }
    }
}
