using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Restaurants
{
    public class RestaurantService(IRestaurantRepository restaurantRepository, ILogger<RestaurantService> logger) : IRestaurantService
    {
        public async Task<IEnumerable<Restaurant>> GetAllRestaurants()
        {
            logger.LogInformation("Retrieving all restaurants");
            var restaurants = await restaurantRepository.GetAllAsync();
            logger.LogInformation("Retrieved {RestaurantCount} restaurants", restaurants.Count());
            return restaurants;
        }
    }
}
