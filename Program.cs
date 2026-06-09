using Engie_powerplant_coding_challenge.Services;
using Engie_powerplant_coding_challenge.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSingleton<IPowerplantCalculator, PowerplantCalculator>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi("/openapi/powerplant/openapi.json")
        .CacheOutput();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
