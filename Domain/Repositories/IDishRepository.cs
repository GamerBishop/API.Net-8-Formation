using Restaurants.Domain.Entities;

namespace Restaurants.Domain.Repositories;

public interface IDishRepository
{
    Task<int> CreateDishAsync(Dish dish);
    Task<bool> UpdateDishAsync(Dish dish);
    Task<bool> DeleteDishAsync(Dish dish);
    Task<IEnumerable<Dish>> GetAllDishesAsync();
}
