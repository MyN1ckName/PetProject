using Microsoft.IdentityModel.Tokens;
using Microsoft.IdentityModel.Logging;
using PetProject.ProductAPI.MongoDb.Extensions;
using PetProject.ProductAPI.Application.Extensions;
using PetProject.ProductAPI.Host.Extensions;
using PetProject.ProductAPI.Host.ExceptionFilters;

var builder = WebApplication.CreateBuilder(args);
builder.Host.AddSerilog(builder.Configuration);
builder.Services.AddControllers(options =>
{
    options.Filters.Add<OperationCancelledExceptionFilter>();
    options.Filters.Add<EntityNotFoundExceptionFilter>();
});
builder.Services.AddProductApiMongoDb(options =>
{
    options.ConnectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING")!;
    options.DatabaseName = Environment.GetEnvironmentVariable("DATABASE_NAME")!;
});
IdentityModelEventSource.ShowPII = true;
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.Authority = Environment.GetEnvironmentVariable("AUTH_SERVER");

        options.AutomaticRefreshInterval = BaseConfigurationManager.MinimumAutomaticRefreshInterval;

        if (builder.Environment.IsDevelopment())
        {
            options.RequireHttpsMetadata = false;
            options.SaveToken = false;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
            };
        }
    });
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("product-api", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("scope", "product-api");
    });
});
builder.Services.AddProductApplication();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure the HTTP request pipeline.
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers()
    .RequireAuthorization("product-api");
app.Run();
