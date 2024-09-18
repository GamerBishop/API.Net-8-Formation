using Restaurants.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Dishes.DTOs
{
    public class DishDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public decimal Price { get; set; }
        public bool IsVegetarian { get; set; }
        public int? KiloCalories { get; set; }

        public static DishDto FromEntity(Dish dish)
        {
            return new DishDto
            {
                Id = dish.Id,
                Name = dish.Name,
                Description = dish.Description,
                Price = dish.Price,
                IsVegetarian = dish.IsVegetarian,
                KiloCalories = dish.KiloCalories
            };
        }

        public static Dish ToEntity(DishDto dishDto)
        {
            return new Dish
            {
                Id = dishDto.Id,
                Name = dishDto.Name,
                Description = dishDto.Description,
                Price = dishDto.Price,
                IsVegetarian = dishDto.IsVegetarian,
                KiloCalories = dishDto.KiloCalories
            };
        }
    }
}
