using PetProject.IdentityServer.Host.Extensions;
using PetProject.IdentityServer.Database.Extensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddIdentityDatabase(options =>
{
    options.ConnectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");
    options.IsDevelopment = true;
});

builder.Host.AddSerilog(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

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
