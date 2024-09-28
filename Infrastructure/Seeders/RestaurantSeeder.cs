﻿using Microsoft.AspNetCore.Identity;
using Microsoft.VisualBasic;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Infrastructure.Persistence;

namespace Restaurants.Infrastructure.Seeders
{
    internal class RestaurantSeeder(RestaurantsDbContext dbContext) : IRestaurantSeeder
    {
        public async Task Seed()
        {
            if (await dbContext.Database.CanConnectAsync())
            {
                if (!dbContext.Restaurants.Any())
                {
                    var restaurants = GetRestaurants();
                    dbContext.Restaurants.AddRange(restaurants);
                    await dbContext.SaveChangesAsync();
                }

                if (!dbContext.Roles.Any())
                {
                    var roles = GetRoles();
                    dbContext.Roles.AddRange(roles);
                    await dbContext.SaveChangesAsync();
                }
            }
        }

        private static IEnumerable<IdentityRole> GetRoles()
        {
            return new List<IdentityRole>
            {
                new IdentityRole { Name = UserRoles.Admin.Name, NormalizedName = UserRoles.Admin.NormalizedName },
                new IdentityRole { Name = UserRoles.Owner.Name, NormalizedName = UserRoles.Owner.NormalizedName },
                new IdentityRole { Name = UserRoles.User.Name, NormalizedName = UserRoles.User.NormalizedName }
            };
        }


        private static IEnumerable<Restaurant> GetRestaurants()
        {
            List<Restaurant> restaurants = [
                new()
            {
                Name = "KFC",
                Category = "Fast Food",
                Description =
                    "KFC (short for Kentucky Fried Chicken) is an American fast food restaurant chain headquartered in Louisville, Kentucky, that specializes in fried chicken.",
                ContactEmail = "contact@kfc.com",
                HasDelivery = true,
                Dishes =
                [
                    new ()
                    {
                        Name = "Nashville Hot Chicken",
                        Description = "Nashville Hot Chicken (10 pcs.)",
                        Price = 10.30M,
                    },

                    new ()
                    {
                        Name = "Chicken Nuggets",
                        Description = "Chicken Nuggets (5 pcs.)",
                        Price = 5.30M,
                    },
                ],
                Adress = new ()
                {
                    City = "London",
                    Street = "Cork St 5",
                    ZipCode = "WC2N 5DU"
                },

            },
            new ()
            {
                Name = "McDonald",
                Category = "Fast Food",
                Description =
                    "McDonald's Corporation (McDonald's), incorporated on December 21, 1964, operates and franchises McDonald's restaurants.",
                ContactEmail = "contact@mcdonald.com",
                HasDelivery = true,
                Adress = new Adress()
                {
                    City = "London",
                    Street = "Boots 193",
                    ZipCode = "W1F 8SR"
                }
            }
            ];

            return restaurants;
        }

    }
}
