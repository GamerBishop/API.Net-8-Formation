using Microsoft.AspNetCore.Mvc;

namespace Restaurants.API.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController(ILogger<WeatherForecastController> p_logger, IWeatherForecastService p_weatherForecastService) : ControllerBase
{
    [HttpGet]
    [Route("ExampleGet")]
    public IEnumerable<WeatherForecast> Get()
    {
        p_logger.LogInformation("Getting weather forecast");
        return p_weatherForecastService.Get( 5, -20, 55 );
    }

    [HttpGet]
    [Route("{take}/ExampleGet")]
    public IActionResult Get([FromQuery] int max, [FromRoute] int take)
    {
        p_logger.LogInformation("Getting weather forecast");

        var result = p_weatherForecastService.Get(take, -20, max);

        //return StatusCode(400, result); => ObjectResult as return value
        return Ok(result);
    }

    [HttpPost]
    [Route("generate")]
    public IActionResult Create([FromQuery] int numberOfResults, [FromBody] Temps temperaturesLimites)
    {
        int minTemperature = temperaturesLimites.MinTemperature;
        int maxTemperature = temperaturesLimites.MaxTemperature;

        if (minTemperature > maxTemperature || numberOfResults < 0)
        {
            p_logger.LogError("Invalid parameters");
            
            p_logger.LogError("Max : {MaxTemperature}. Min : {MinTemperature}. Number of Results to be generated {NumberOfResults}", maxTemperature, minTemperature, numberOfResults);
            return BadRequest("Invalid parameters");
        }

        p_logger.LogInformation("Generating weather forecast");

        var result = p_weatherForecastService.Get(numberOfResults, minTemperature, maxTemperature);

        return Ok(result);
    }

    [HttpGet("CurrentDay")]
    public WeatherForecast GetCurrent()
    {
        p_logger.LogInformation("Gettin55 -20 55g weather forecast");
        return p_weatherForecastService.Get(1, -20, 55).First();
    }

    [HttpPost]
    public string Hello([FromBody] string name) => $"Hello {name}";

}