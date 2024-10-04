using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Persistence;

namespace Restaurants.Infrastructure.Repositories;
internal class RestaurantRepository(RestaurantsDbContext context) : IRestaurantRepository
{
    public async Task<IEnumerable<Restaurant>> GetAllAsync()
    {
         var restaurants = await context.Restaurants.Include(r => r.Dishes).ToListAsync();
        return restaurants;
    }

    public async Task<IEnumerable<Restaurant>> GetAllMatchingAsync(string? _searchPhrase)
    {
        var lowerCaseSearchPhrase = _searchPhrase?.ToLower();
        var restaurants = await context.Restaurants
            .Include(r => r.Dishes)
            .Where(r => string.IsNullOrEmpty(lowerCaseSearchPhrase) || (r.Name.ToLower().Contains(lowerCaseSearchPhrase) ||
                                                                        r.Description.ToLower().Contains(lowerCaseSearchPhrase)))
            .ToListAsync();
        return restaurants;
    }

    public async Task<Restaurant?> GetByIdAsync(Guid id)
    {
        var restaurant = await context.Restaurants
            .Include(r => r.Dishes)
            .FirstOrDefaultAsync(r => r.Id == id);
        return restaurant;
    }
    public async Task<Guid> CreateAsync(Restaurant restaurant)
    {
        await context.Restaurants.AddAsync(restaurant);
        await context.SaveChangesAsync();
        return restaurant.Id;
    }

    public async Task<Boolean> DeleteAsync(Restaurant restaurant)
    {
        context.Restaurants.Remove(restaurant);
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<Boolean> UpdateAsync(Restaurant restaurant)
    {
        context.Restaurants.Update(restaurant);
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<int> GetRestaurantsCountCreatedByUser(string userId)
    {
        return await context.Restaurants.CountAsync(r => r.OwnerId == userId);
    }


}
