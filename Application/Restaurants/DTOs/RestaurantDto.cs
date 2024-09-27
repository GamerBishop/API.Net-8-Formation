using Restaurants.Application.Dishes.DTOs;
using Restaurants.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Restaurants.DTOs
{
    /// <summary>
    /// Data Transfer Object for representing a restaurant.
    /// </summary>
    public class RestaurantDto
    {
        /// <summary>
        /// Gets or sets the unique identifier for the restaurant.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the restaurant.
        /// </summary>
        public string Name { get; set; } = default!;

        /// <summary>
        /// Gets or sets the description of the restaurant.
        /// </summary>
        public string Description { get; set; } = default!;

        /// <summary>
        /// Gets or sets the category of the restaurant.
        /// </summary>
        public string Category { get; set; } = default!;

        /// <summary>
        /// Gets or sets a value indicating whether the restaurant offers delivery service.
        /// </summary>
        public bool HasDelivery { get; set; }

        /// <summary>
        /// Gets or sets the city where the restaurant is located.
        /// </summary>
        public string? City { get; set; }

        /// <summary>;         
        /// Gets or sets the street address of the restaurant.
        /// </summary>
        public string? Street { get; set; }
        /// <summary>        /// Gets or sets the zip code of the restaurant's location.
        /// </summary>
        public string? ZipCode { get; set; }

        /// <summary>
        /// Gets or sets the list of dishes offered by the restaurant.
        /// </summary>
        public List<DishDto> Dishes { get; set; } = [];
    }
}

