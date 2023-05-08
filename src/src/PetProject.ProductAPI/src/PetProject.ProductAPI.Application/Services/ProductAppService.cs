using PetProject.ProductAPI.Application.Contracts.Dto.Product;
using PetProject.ProductAPI.Application.Contracts.Interfaces;

namespace PetProject.ProductAPI.Application.Services;

public class ProductAppService : IProductService
{
    public Task<List<ProductDto>> GetAllAsync()
    {
        throw new NotImplementedException();
    }
}
