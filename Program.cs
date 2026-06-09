using Engie_powerplant_coding_challenge.Helpers;
using Engie_powerplant_coding_challenge.Services;
using Engie_powerplant_coding_challenge.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSingleton<IPowerplantCalculator, PowerplantCalculator>();
builder.Services.AddControllers().AddJsonOptions(opts => opts.JsonSerializerOptions.Converters.Add(new PowerplantTypeConverter()));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi("/openapi/powerplant/openapi.json")
        .CacheOutput();
}

app.UseHttpsRedirection();

app.UseMiddleware<JsonExceptionMiddleware>();

app.MapControllers();

app.Run();
