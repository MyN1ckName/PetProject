using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using PetProject.ProductAPI.Application.Contracts.Dto;
using PetProject.ProductAPI.Application.Contracts.Interfaces;
using PetProject.ProductAPI.Application.Contracts.Dto.Manufacturer;

namespace PetProject.ProductAPI.Host.Controllers;

[ApiController]
[Authorize(Policy = "manufacturer-api")]
[Route("api/[controller]")]
public class ManufacturerController : ControllerBase
{
    private readonly ILogger _logger;
    private readonly IManufacturerService _manufacturerService;

    public ManufacturerController(
        ILogger<ManufacturerController> logger,
        IManufacturerService manufacturerService)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _manufacturerService = manufacturerService ?? throw new ArgumentNullException(nameof(manufacturerService));
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
    {
        var manufactures = await _manufacturerService.GetAllAsync(cancellationToken);
        return Ok(manufactures);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        var manufacture = await _manufacturerService.GetAsync(id, cancellationToken);
        return Ok(manufacture);
    }

    [HttpPost]
    public async Task<IActionResult> InsertOneAsync(CreateManufacturerDto input)
    {
        var id = await _manufacturerService.InsertOneAsync(input);
        return StatusCode(StatusCodes.Status201Created, new EntityDto<Guid> { Id = id });
    }

    [HttpPut]
    public async Task<IActionResult> ReplaceOneAsync(ManufacturerDto manufacturer, CancellationToken cancellationToken)
    {
        await _manufacturerService.ReplaceOneAsync(manufacturer, cancellationToken);
        return StatusCode(StatusCodes.Status204NoContent);
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> DeleteOneAsync(Guid id)
    {
        await _manufacturerService.DeleteOneAsync(id);
        return StatusCode(StatusCodes.Status204NoContent);
    }
}
