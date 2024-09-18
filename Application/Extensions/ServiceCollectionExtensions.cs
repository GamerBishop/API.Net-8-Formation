
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Application.Restaurants;

namespace Restaurants.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddApplication(this IServiceCollection services)
    {

        System.Reflection.Assembly assembly = typeof(ServiceCollectionExtensions).Assembly;

        services.AddScoped<IRestaurantService, RestaurantService>();
        services.AddAutoMapper(assembly);
        services.AddValidatorsFromAssemblies([assembly]).AddFluentValidationAutoValidation();
    }
}

