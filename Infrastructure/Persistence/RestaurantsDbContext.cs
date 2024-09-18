﻿using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Infrastructure.Persistence
{
    internal class RestaurantsDbContext : DbContext
    {
        internal DbSet<Restaurant> Restaurants { get; set; }
        internal DbSet<Dish> Dishes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=Restaurants;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // Owned entity : Not a separate table, No Id property, No explicit foreign key 
            modelBuilder.Entity<Restaurant>()
                .OwnsOne(p => p.Adress);
            // One to many relationship
            modelBuilder.Entity<Restaurant>()
                .HasMany(p => p.Dishes)
                .WithOne()
                .HasForeignKey(d => d.RestaurantId);

        }

    }
}