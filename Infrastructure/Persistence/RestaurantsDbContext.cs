using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Infrastructure.Persistence
{
    internal class RestaurantsDbContext(DbContextOptions<RestaurantsDbContext> dbContextOptions) : IdentityDbContext<User>(dbContextOptions)
    {
        internal DbSet<Restaurant> Restaurants { get; set; }
        internal DbSet<Dish> Dishes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Owned entity : Not a separate table, No Id property, No explicit foreign key 
            modelBuilder.Entity<Restaurant>()
                .OwnsOne(p => p.Adress);
            // One to many relationship
            modelBuilder.Entity<Restaurant>()
                .HasMany(p => p.Dishes)
                .WithOne()
                .HasForeignKey(d => d.RestaurantId);

            modelBuilder.Entity<User>()
                .HasMany(p => p.OwnedRestaurants)
                .WithOne(r => r.Owner)
                .HasForeignKey(r => r.OwnerId);
        }

    }
}
