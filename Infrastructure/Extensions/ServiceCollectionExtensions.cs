﻿using Microsoft.Extensions.DependencyInjection;
using Restaurants.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Restaurants.Infrastructure.Seeders;
using Restaurants.Infrastructure.Repositories;
using Restaurants.Domain.Repositories;
using Restaurants.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Restaurants.Infrastructure.Authorization;
using Restaurants.Infrastructure.Constants;
using Microsoft.AspNetCore.Authorization;
using Restaurants.Infrastructure.Authorization.Services;
using Restaurants.Domain.Interfaces;
using Restaurants.Infrastructure.Authorization.Requirements.MinimumAge;
using Restaurants.Infrastructure.Authorization.Requirements.MinimumRestaurantsCreated;

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
            .AddClaimsPrincipalFactory<RestaurantsUserClaimsPrincipalFactory>()
            .AddEntityFrameworkStores<RestaurantsDbContext>();

            services.AddScoped<IRestaurantSeeder, RestaurantSeeder>();

            services.AddScoped<IRestaurantRepository, RestaurantRepository>();
            services.AddScoped<IDishRepository, DishRepository>();

            // Authorization part
            services.AddAuthorizationBuilder()
                // Only checks if value exists
                //.AddPolicy("HasNationality", builder => builder.RequireClaim("Nationality"));
                // Checks the value of the specified claim
                .AddPolicy(PolicyNames.s_HasNationality, builder => builder.RequireClaim(AppClaimTypes.s_Nationality, "German", "Polish"))
                .AddPolicy(PolicyNames.s_AtLeast20, builder => builder.AddRequirements(new MinimumAgeRequirement(20)))
                .AddPolicy(PolicyNames.s_AtLeast2Restaurants, builder => builder.AddRequirements(new MinimumRestaurantsCreatedRequirement(2)));
            services.AddScoped<IAuthorizationHandler, MinimumAgeRequirementHandler>();
            services.AddScoped<IAuthorizationHandler, MinimumRestaurantsCreatedRequirementHandler>();
            services.AddScoped<IRestaurantAuthorizationService, RestaurantAuthorizationService>();
        }
    }
}
