using System.Diagnostics;

namespace Restaurants.API.Middlewares;

/// <summary>
/// Une nouvelle classe qui implémente l'interface IMiddleware.
/// La méthode InvokeAsync est utilisée pour logger les détails des requêtes, les heures d’appels et le temps d'exécution de la requête.
/// </summary>
public class TimeLoggingMiddleware(ILogger<TimeLoggingMiddleware> logger) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        //Log request, start datetime
        logger.LogInformation($"Request {context.Request.Method} {context.Request.Path} started at {DateTime.Now}");

        var watch = new Stopwatch();
        watch.Start();

        await next(context);

        watch.Stop();
        //Log request, end datetime
        logger.LogInformation($"Request {context.Request.Method} {context.Request.Path} ended at {DateTime.Now}");
        logger.LogInformation($"Request {context.Request.Method} {context.Request.Path} executed in {watch.ElapsedMilliseconds} ms");
    }
}
