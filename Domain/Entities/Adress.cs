using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Domain.Entities
{
    /// <summary>
    /// Represents an address with a city, street, and zip code.
    /// </summary>
    public class Adress
    {
        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        public string? City { get; set; }
        /// <summary>
        /// Gets or sets the street.
        /// </summary>
        public string? Street { get; set; }
        /// <summary>
        /// Gets or sets the zip code.
        /// </summary>
        public string? ZipCode { get; set; }
    }
}
