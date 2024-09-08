using Microsoft.AspNetCore.Mvc;

namespace Restaurants.API.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{

    private readonly ILogger<WeatherForecastController> _logger;

    private readonly WeatherForecastService p_WeatherForecastService;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, WeatherForecastService weatherForecastService)
    {
        _logger = logger;
        p_WeatherForecastService = weatherForecastService;


        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            return p_WeatherForecastService.Get();
        }
    }

}
