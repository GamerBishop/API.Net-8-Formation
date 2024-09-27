using Restaurants.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Dishes.DTOs
{
    /// <summary>
    /// Represents a data transfer object for a dish.
    /// </summary>
    public class DishDto
    {
        /// <summary>
        /// Gets or sets the unique identifier for the dish.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the dish.
        /// </summary>
        public string Name { get; set; } = default!;

        /// <summary>
        /// Gets or sets the description of the dish.
        /// </summary>
        public string Description { get; set; } = default!;

        /// <summary>
        /// Gets or sets the price of the dish.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the dish is vegetarian.
        /// </summary>
        public bool IsVegetarian { get; set; }

        /// <summary>
        /// Gets or sets the number of kilocalories in the dish.
        /// </summary>
        public int? KiloCalories { get; set; }
    }
}
