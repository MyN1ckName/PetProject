using PetProject.ProductAPI.MongoDb.Extensions;
using PetProject.ProductAPI.Application.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddProductApiMongoDb(options =>
{
    options.ConnectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");
    options.DatabaseName = Environment.GetEnvironmentVariable("DATABASE_NAME");
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

app.UseAuthorization();

app.MapControllers();

app.Run();
