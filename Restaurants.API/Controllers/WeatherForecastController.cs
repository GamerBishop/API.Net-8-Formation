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
        return p_WeatherForecastService.Get( 5, -20, 55 );
    }

    [HttpGet]
    [Route("{take}/ExampleGet")]
    public IActionResult Get([FromQuery] int max, [FromRoute] int take)
    {
        _logger.LogInformation("Getting weather forecast");

        //if (take > 35000)
        //{
        //    Response.StatusCode = 400;
        //}

        var result = p_WeatherForecastService.Get(5, -20, 55);

        //return StatusCode(400, result); => ObjectResult as return value
        return BadRequest(result);
    }

    [HttpGet("CurrentDay")]
    public WeatherForecast GetCurrent()
    {
        _logger.LogInformation("Gettin55 -20 55g weather forecast");
        return p_WeatherForecastService.Get(1, -20, 55).First();
    }

    [HttpPost]
    public string Hello([FromBody] string name) => $"Hello {name}";

}