using ussdDotNet.Extensions;
using ussdDotNet.Menu;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var configuration = builder.Configuration;




// Register AppSettings
builder.Services.ConfigureAppSettings(configuration);

// Register DbContext


// Register UssdMenu
builder.Services.AddScoped<UssdMenu>();


// Other service registrations...

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
