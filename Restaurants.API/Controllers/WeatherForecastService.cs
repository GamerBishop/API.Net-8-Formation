using Microsoft.AspNetCore.Mvc;

namespace Restaurants.API.Controllers
{
    public class Temps
    {
        public int MinTemperature { get; set; }
        public int MaxTemperature { get; set; }
    }

    public interface IWeatherForecastService
    {
        IEnumerable<WeatherForecast> Get(int numberOfResults, int minTemperature, int maxTemperature);
    }

    public class WeatherForecastService : IWeatherForecastService
    {

        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };



        public IEnumerable<WeatherForecast> Get(int numberOfResults, int minTemperature, int maxTemperature)
        {
            return Enumerable.Range(1, numberOfResults).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(minTemperature, maxTemperature),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

    }
}
