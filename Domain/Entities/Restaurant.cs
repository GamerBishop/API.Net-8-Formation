using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Domain.Entities
{
    /// <summary>
    /// Représente un restaurant.
    /// </summary>
    public class Restaurant
    {
        /// <summary>
        /// Obtient ou définit l'identifiant du restaurant.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Obtient ou définit le nom du restaurant.
        /// </summary>
        public string Name { get; set; } = default!;

        /// <summary>
        /// Obtient ou définit la description du restaurant.
        /// </summary>
        public string Description { get; set; } = default!;

        /// <summary>
        /// Obtient ou définit la catégorie du restaurant.
        /// </summary>
        public string Category { get; set; } = default!;

        /// <summary>
        /// Obtient ou définit une valeur indiquant si le restaurant propose la livraison.
        /// </summary>
        public bool HasDelivery { get; set; }

        /// <summary>
        /// Obtient ou définit l'adresse e-mail de contact du restaurant.
        /// </summary>
        public string? ContactEmail { get; set; }

        /// <summary>
        /// Obtient ou définit le numéro de contact du restaurant.
        /// </summary>
        public string? ContactNumber { get; set; }

        /// <summary>
        /// Obtient ou définit l'adresse du restaurant.
        /// </summary>
        public Adress? Adress { get; set; }

        /// <summary>
        /// Obtient ou définit la liste des plats proposés par le restaurant.
        /// </summary>
        public List<Dish> Dishes { get; set; } = [];

        /// <summary>
        /// Restaurant's owner and manager.
        /// </summary>
        public User Owner { get; set; } = default!;

        /// <summary>
        /// Id of the owner user.
        /// </summary>
        public string OwnerId { get; set; } = default!;
    }
}
