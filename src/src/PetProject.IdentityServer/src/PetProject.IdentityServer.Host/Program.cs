using PetProject.IdentityServer.Host.Extensions;
using PetProject.IdentityServer.Database.Extensions;
using Serilog;
using Microsoft.AspNetCore.HttpOverrides;
using System.Net;
using Duende.IdentityServer.Services;
using PetProject.IdentityServer.Host.ProfileServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddIdentityDatabase(options =>
{
    options.ConnectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");
    options.IsDevelopment = builder.Environment.IsDevelopment();
});

builder.Host.AddSerilog(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

builder.Services.AddScoped<IProfileService, MachineIdProfileService>();

var app = builder.Build();

app.UseForwardedHeaders();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (app.Environment.IsDevelopment() is false)
    app.UseHttpsRedirection();

app.UseRouting();

app.UseIdentityServer();

app.UseAuthorization();
app.MapControllers();

app.Run();
