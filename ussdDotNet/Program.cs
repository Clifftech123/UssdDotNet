using Microsoft.EntityFrameworkCore;
using ussdDotNet.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var configuration = builder.Configuration;

// Load configuration from appsettings.json
configuration.AddJsonFile("appsettings.json");

// Configure AppSettings and DbContext
builder.Services.ConfigureAppSettings(configuration);
builder.Services.ConfigureDbContext(configuration.GetConnectionString("USSDConnection"));

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

await app.RunAsync();
