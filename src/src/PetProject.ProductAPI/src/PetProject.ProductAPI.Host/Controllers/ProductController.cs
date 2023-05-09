using Microsoft.AspNetCore.Mvc;
using PetProject.ProductAPI.Application.Contracts.Interfaces;
using PetProject.ProductAPI.Application.Contracts.Dto.Product;

namespace PetProject.ProductAPI.Host.Controllers
{
    [Route("/api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IProductAppService _productAppService;

        public ProductController(
            ILogger<ProductController> logger,
            IProductAppService productAppService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _productAppService = productAppService ?? throw new ArgumentNullException(nameof(productAppService));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var products = await _productAppService.GetAllAsync();
            return Ok(products);
        }

        [HttpPost]
        public async Task<IActionResult> InsertOneAsync([FromBody] CreateProductDto input)
        {
            await _productAppService.InsertOneAsync(input);
            return StatusCode(StatusCodes.Status201Created);
        }
    }
}
