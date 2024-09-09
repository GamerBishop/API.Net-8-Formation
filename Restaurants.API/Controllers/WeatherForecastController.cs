using Microsoft.AspNetCore.Mvc;

namespace Restaurants.API.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly ILogger<WeatherForecastController> _logger;

    private readonly IWeatherForecastService p_WeatherForecastService = new WeatherForecastService();


    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    [Route("ExampleGet")]
    public IEnumerable<WeatherForecast> Get()
    {
        _logger.LogInformation("Getting weather forecast");
        return p_WeatherForecastService.Get();
    }

    [HttpGet]
    [Route("{take}/ExampleGet")]
    public IEnumerable<WeatherForecast> Get([FromQuery] int max, [FromRoute] int take)
    {
        _logger.LogInformation("Getting weather forecast");
        return p_WeatherForecastService.Get();
    }

    [HttpGet("CurrentDay")]
    public WeatherForecast GetCurrent()
    {
        _logger.LogInformation("Getting weather forecast");
        return p_WeatherForecastService.Get().First();
    }

    [HttpPost]
    public string Hello([FromBody] string name) => $"Hello {name}";

}
