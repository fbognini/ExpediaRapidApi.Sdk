using ExpediaRapidApi.Sdk;
using ExpediaRapidApi.Sdk.Cars.GetCarAvailability;
using Microsoft.AspNetCore.Mvc;
using Serilog;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddSerilog((configuration) =>
{
    configuration
        .ReadFrom.Configuration(builder.Configuration);
});

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddExpediaRapidApiService(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", async ([FromServices] ExpediaRapidApiClient client) =>
{
    var request = new GetCarAvailabilityRequest()
    {
        Currency = "USD",
        Language = "it-IT",
        PickupTime = "2025-10-10T08:00:00",
        DropoffTime = "2025-10-11T08:00:00",
        PickupArea = "10,45.464664,9.188540",
        DropoffArea = "10,45.464664,9.188540",
        DriverAge = 18,
        Limit = 200,
        SalesChannel = CarsSalesChannel.website,
        SalesEnvironment = CarsSalesEnvironment.car_only,
        CountryCode = "IT",
        Sort = CarsSort.recommended,
    };

    var availabilitys = await client.Cars.GetCarAvailabilityAsync(request);
    return availabilitys;

})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
