using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using PetProject.ProductAPI.Application.Contracts.Interfaces;
using PetProject.ProductAPI.Application.Contracts.Dto.Product;

namespace PetProject.ProductAPI.Host.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    [Authorize]
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
        [Route("{id}")]
        public async Task<IActionResult> GetAsync(Guid id, CancellationToken cancellationToken)
        {
            var product = await _productAppService.GetAsync(id, cancellationToken);
            return Ok(product);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
        {
            var products = await _productAppService.GetAllAsync(cancellationToken);
            return Ok(products);
        }

        [HttpPost]
        public async Task<IActionResult> InsertOneAsync([FromBody] CreateProductDto input, CancellationToken cancellationToken)
        {
            var id = await _productAppService.InsertOneAsync(input, cancellationToken);
            return StatusCode(StatusCodes.Status201Created, new { Id = id });
        }

        [HttpPut]
        public async Task<IActionResult> UpdateOneAsync([FromBody] ProductDto product, CancellationToken cancellationToken)
        {
            await _productAppService.UpdateOneAsync(product, cancellationToken);
            return StatusCode(StatusCodes.Status204NoContent);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteOneAsync(Guid id)
        {
            await _productAppService.DeleteOneAsync(id);
            return StatusCode(StatusCodes.Status204NoContent);
        }
    }
}
