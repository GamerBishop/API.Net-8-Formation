using Microsoft.Extensions.DependencyInjection;
using Restaurants.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Restaurants.Infrastructure.Seeders;

namespace Restaurants.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<RestaurantsDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("RestaurantsDB")));

            services.AddScoped<IRestaurantSeeder, RestaurantSeeder>();
        }
    }
}
