using Microsoft.AspNetCore.Mvc;

namespace BerryJuice.WebAPI.Controllers;

[ApiController]
[Route(template: "[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries =
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching",
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        return Enumerable
              .Range(start: 1, count: 5)
              .Select(
                   selector: index => new WeatherForecast
                   {
                       Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                       TemperatureC = Random.Shared.Next(minValue: -20, maxValue: 55),
                       Summary = Summaries[Random.Shared.Next(Summaries.Length)],
                   }
               )
              .ToArray();
    }
}
