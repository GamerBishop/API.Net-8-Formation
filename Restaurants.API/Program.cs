using Restaurants.API.Controllers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();


//////////////////////////////////////
// Singleton, Scoped ou Transient ? //
//////////////////////////////////////

/* Cette méthode configure le service pour qu'une nouvelle instance 
de WeatherForecastService soit créée chaque fois que le service est demandé. 
Cela signifie que chaque fois que vous injectez WeatherForecastService dans un 
contrôleur ou un autre service, une nouvelle instance sera fournie. 
Ce cycle de vie est utile pour les services légers et stateless (sans état).*/
//builder.Services.AddTransient<IWeatherForecastService, WeatherForecastService>();

/* Avec cette méthode, une seule instance de WeatherForecastService 
est créée par requête HTTP. Cela signifie que pour une requête donnée, 
toutes les injections de WeatherForecastService partageront la même instance. 
Ce cycle de vie est approprié pour les services qui doivent conserver des données 
spécifiques à une requête, mais qui ne doivent pas être partagés entre différentes requêtes.*/
// Ici, on souhaitera utiliser cette méthode pour le service WeatherForecastService
builder.Services.AddScoped<IWeatherForecastService, WeatherForecastService>();

/* Cette méthode configure le service pour qu'une seule instance de WeatherForecastService 
soit créée et partagée dans toute l'application. Cette instance unique est créée la première 
fois qu'elle est demandée, puis réutilisée pour toutes les requêtes suivantes. 
Ce cycle de vie est idéal pour les services qui doivent conserver un état global ou 
qui sont coûteux à créer.*/
//builder.Services.AddSingleton<IWeatherForecastService, WeatherForecastService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
