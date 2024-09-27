using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Persistence;

namespace Restaurants.Infrastructure.Repositories;

internal class DishRepository(RestaurantsDbContext context) : IDishRepository
{
    public async Task<int> CreateDishAsync(Dish dish)
    {
        context.Dishes.Add(dish);
        await context.SaveChangesAsync();
        return dish.Id;
    }

    public async Task<bool> DeleteDishAsync(Dish dish)
    {
        context.Dishes.Remove(dish);
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<Dish>> GetAllDishesAsync()
    {
        var dishes = await context.Dishes.ToListAsync();
        return dishes;
    }

    public async Task<bool> UpdateDishAsync(Dish dish)
    {
        context.Dishes.Update(dish);
        await context.SaveChangesAsync();
        return true;
    }
}
