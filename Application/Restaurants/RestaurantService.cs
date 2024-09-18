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

        public async Task<Restaurant?> GetRestaurantById(Guid id)
        {
            logger.LogInformation("Retrieving restaurant with id {RestaurantId}", id);
            var restaurant = await restaurantRepository.GetByIdAsync(id);
            if (restaurant == null)
            {
                logger.LogWarning("Restaurant with id {RestaurantId} was not found", id);
                
            }
            else
            {
                logger.LogInformation("Retrieved restaurant with id {RestaurantId}", id);
            }
            return restaurant;
        }

    }
}
