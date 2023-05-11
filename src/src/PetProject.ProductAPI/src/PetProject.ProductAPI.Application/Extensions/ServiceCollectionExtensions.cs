using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using PetProject.ProductAPI.Application.Services;
using PetProject.ProductAPI.Application.Contracts.Interfaces;

namespace PetProject.ProductAPI.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddProductApplication(
        this IServiceCollection services)
    {
        var config = new MapperConfiguration(cfg => cfg.AddProfile<AutomapperProfile>());
        var mapper = config.CreateMapper();
        services.AddSingleton<IMapper>(mapper);
        services.AddScoped<IProductAppService, ProductAppService>();

        return services;
    }
}
