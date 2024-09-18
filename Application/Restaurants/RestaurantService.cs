using Microsoft.Extensions.Logging;
using Restaurants.Application.Dishes.DTOs;
using Restaurants.Application.Restaurants.DTOs;
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
        public async Task<IEnumerable<RestaurantDto>> GetAllRestaurants()
        {
            logger.LogInformation("Retrieving all restaurants");
            var restaurants = await restaurantRepository.GetAllAsync();
            logger.LogInformation("Retrieved {RestaurantCount} restaurants", restaurants.Count());

            var restaurantsDtos = restaurants.Select(restaurant => new RestaurantDto
            {
                Id = restaurant.Id,
                Name = restaurant.Name,
                Description = restaurant.Description,
                Category = restaurant.Category,
                HasDelivery = restaurant.HasDelivery,
                City = restaurant.Adress?.City,
                Street = restaurant.Adress?.Street,
                ZipCode = restaurant.Adress?.ZipCode,
                Dishes = restaurant.Dishes.Select(dish => new DishDto
                {
                    Id = dish.Id,
                    Name = dish.Name,
                    Description = dish.Description,
                    Price = dish.Price
                }).ToList()
            });

            return restaurantsDtos;
        }

        public async Task<RestaurantDto?> GetRestaurantById(Guid id)
        {
            logger.LogInformation("Retrieving restaurant with id {RestaurantId}", id);
            var restaurant = await restaurantRepository.GetByIdAsync(id);
            if (restaurant == null)
            {
                logger.LogWarning("Restaurant with id {RestaurantId} was not found", id);
                return null;
            }
            else
            {
                logger.LogInformation("Retrieved restaurant with id {RestaurantId}", id);
            }
            var restaurantDto = RestaurantDto.FromEntity(restaurant);

            return restaurantDto;
        }

    }
}
