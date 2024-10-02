using Restaurants.Domain.Entities;

namespace Restaurants.Domain.Repositories;

/// <summary>
/// Represents a repository for managing restaurants.
/// </summary>
public interface IRestaurantRepository
{
    /// <summary>
    /// Retrieves all restaurants asynchronously.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains the collection of restaurants.</returns>
    Task<IEnumerable<Restaurant>> GetAllAsync();

    /// <summary>
    /// Retrieves a restaurant by its ID asynchronously.
    /// </summary>
    /// <param name="id">The ID of the restaurant to retrieve.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the restaurant, or null if not found.</returns>
    Task<Restaurant?> GetByIdAsync(Guid id);

    /// <summary>
    /// Creates a new restaurant asynchronously.
    /// </summary>
    /// <param name="restaurant">The restaurant to create.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the ID of the created restaurant.</returns>
    Task<Guid> CreateAsync(Restaurant restaurant);

    /// <summary>
    /// Updates an existing restaurant asynchronously.
    /// </summary>
    /// <param name="restaurant">The restaurant to update.</param>
    /// <returns>A task that represents the asynchronous operation. The task result indicates whether the update was successful or not.</returns>
    Task<bool> UpdateAsync(Restaurant restaurant);

    /// <summary>
    /// Deletes a restaurant asynchronously.
    /// </summary>
    /// <param name="restaurant">The restaurant to delete.</param>
    /// <returns>A task that represents the asynchronous operation. The task result indicates whether the deletion was successful or not.</returns>
    Task<bool> DeleteAsync(Restaurant restaurant);

    Task<int> GetRestaurantsCountCreatedByUser(string userId);
}
