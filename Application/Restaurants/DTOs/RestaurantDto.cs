using Restaurants.Application.Dishes.DTOs;
using Restaurants.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Restaurants.DTOs
{
    public class RestaurantDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string Category { get; set; } = default!;
        public bool HasDelivery { get; set; }

        public string? City { get; set; }
        public string? Street { get; set; }
        public string? ZipCode { get; set; }

        public List<DishDto> Dishes { get; set; } = [];


        public static RestaurantDto? FromEntity(Restaurant restaurant)
        {
            if (restaurant == null) return null;

            return new RestaurantDto
            {
                Id = restaurant.Id,
                Name = restaurant.Name,
                Description = restaurant.Description,
                Category = restaurant.Category,
                HasDelivery = restaurant.HasDelivery,
                City = restaurant.Adress?.City,
                Street = restaurant.Adress?.Street,
                ZipCode = restaurant.Adress?.ZipCode,
                Dishes = restaurant.Dishes.Select(dish => DishDto.FromEntity(dish)).ToList()
            };
        }

        public static Restaurant? ToEntity(RestaurantDto restaurantDto)
        {
            if (restaurantDto == null) return null;

            return new Restaurant
            {
                Id = restaurantDto.Id,
                Name = restaurantDto.Name,
                Description = restaurantDto.Description,
                Category = restaurantDto.Category,
                HasDelivery = restaurantDto.HasDelivery,
                Adress = new Adress
                {
                    City = restaurantDto.City,
                    Street = restaurantDto.Street,
                    ZipCode = restaurantDto.ZipCode
                },
                Dishes = restaurantDto.Dishes.Select(dishDto => DishDto.ToEntity(dishDto)).ToList()
            };
        }
    }
}

