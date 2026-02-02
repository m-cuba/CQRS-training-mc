using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Sportsbook.ServiceTemplate.API.Controllers;

public class ExampleController(ILogger<ExampleController> logger) : ApiController
{
    [HttpGet("{userName}", Name = "GetWeatherForecast")]
    [ProducesResponseType(typeof(WeatherForecast[]), (int)HttpStatusCode.OK)]
    public ActionResult<WeatherForecast[]> GetWeatherForecast()
    {
        var summaries = new[]{
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        var forecast = Enumerable.Range(1, 5).Select(index =>
            new WeatherForecast
            (
                DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                Random.Shared.Next(-20, 55),
                summaries[Random.Shared.Next(summaries.Length)]
            ))
        .ToArray();

        logger.LogInformation("GetWeatherForecast executed");

        return Ok(forecast);
    }


    public record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
    {
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
    }

}