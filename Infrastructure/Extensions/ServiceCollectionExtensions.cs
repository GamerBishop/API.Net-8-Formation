using Microsoft.Extensions.DependencyInjection;
using Restaurants.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Restaurants.Infrastructure.Seeders;
using Restaurants.Infrastructure.Repositories;
using Restaurants.Domain.Repositories;
using Restaurants.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Restaurants.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<RestaurantsDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("RestaurantsDB"))
                .EnableSensitiveDataLogging());

            services.AddIdentityApiEndpoints<User>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<RestaurantsDbContext>();

            services.AddScoped<IRestaurantSeeder, RestaurantSeeder>();

            services.AddScoped<IRestaurantRepository, RestaurantRepository>();
            services.AddScoped<IDishRepository, DishRepository>();
        }
    }
}
