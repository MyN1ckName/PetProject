using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PetProject.ProductAPI.Domain.Exceptions;

namespace PetProject.ProductAPI.Host.ExceptionFilters;

public class EntityNotFoundExceptionFilter : ExceptionFilterAttribute
{
    private readonly ILogger _logger;

    public EntityNotFoundExceptionFilter(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<EntityNotFoundExceptionFilter>();
    }
    public override void OnException(ExceptionContext context)
    {
        if (context.Exception is EntityNotFoundException ex)
        {
            _logger.LogInformation("{ExceptionType} was throw. Message: {Message}", nameof(EntityNotFoundException), ex.Message);
            context.ExceptionHandled = true;
            context.Result = new StatusCodeResult(404);
        }
    }
}
