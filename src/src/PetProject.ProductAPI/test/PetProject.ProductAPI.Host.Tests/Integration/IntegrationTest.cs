namespace PetProject.ProductAPI.Host.Tests.Integration;

public abstract class IntegrationTest
{
    protected static ILogger<T> CreateTestLogger<T>() => new LoggerFactory().CreateLogger<T>();
    protected static IMapper CreateTestMapper<T>() where T: Profile, new() =>
        new MapperConfiguration(cfg => cfg.AddProfile<T>()).CreateMapper();
}
