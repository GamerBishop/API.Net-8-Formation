using AutoMapper;
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
    public class RestaurantService(IRestaurantRepository restaurantRepository, ILogger<RestaurantService> logger, IMapper mapper) : IRestaurantService
    {
        public async Task<IEnumerable<RestaurantDto>> GetAllRestaurants()
        {
            logger.LogInformation("Retrieving all restaurants");
            var restaurants = await restaurantRepository.GetAllAsync();
            logger.LogInformation("Retrieved {RestaurantCount} restaurants", restaurants.Count());

            var restaurantsDtos = mapper.Map<IEnumerable<RestaurantDto>>(restaurants);

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

            var restaurantDto = mapper.Map<RestaurantDto>(restaurant);

            return restaurantDto;
        }

        public async Task<Guid> CreateRestaurant(CreateRestaurantDto createRestaurantDto)
        {
            logger.LogInformation("Creating restaurant with name {RestaurantName}", createRestaurantDto.Name);

            var restaurant = mapper.Map<Restaurant>(createRestaurantDto);

            Guid createdGuid = await restaurantRepository.CreateAsync(restaurant);

            logger.LogInformation("Created restaurant with name {RestaurantName} and Id : {Guid} ", createRestaurantDto.Name, createdGuid);
            return createdGuid;
        }

    }
}
