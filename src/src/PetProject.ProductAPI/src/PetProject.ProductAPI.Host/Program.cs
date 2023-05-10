using Microsoft.IdentityModel.Tokens;
using PetProject.ProductAPI.MongoDb.Extensions;
using PetProject.ProductAPI.Application.Extensions;
using Microsoft.IdentityModel.Logging;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddProductApiMongoDb(options =>
{
    options.ConnectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");
    options.DatabaseName = Environment.GetEnvironmentVariable("DATABASE_NAME");
});

IdentityModelEventSource.ShowPII = true;
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.Authority = Environment.GetEnvironmentVariable("AUTH_SERVER");

        if (builder.Environment.IsDevelopment())
        {
            options.RequireHttpsMetadata = false;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidIssuer = Environment.GetEnvironmentVariable("VALID_ISSUER"),
            };
        }
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("product-scope", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("scope", "product-scope");
    });
});

builder.Services.AddProductApplicatio();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
