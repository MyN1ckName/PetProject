namespace PetProject.IdentityServer.Host.Middlewares;
internal class OneActiveDeviceMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger _logger;

    public OneActiveDeviceMiddleware(
        RequestDelegate next,
        ILogger<OneActiveDeviceMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        _logger.LogError($"ping {nameof(OneActiveDeviceMiddleware)}");

        // Call the next delegate/middleware in the pipeline.
        await _next(context);
    }
}
