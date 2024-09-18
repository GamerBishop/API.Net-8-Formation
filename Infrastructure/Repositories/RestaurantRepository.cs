using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Persistence;

namespace Restaurants.Infrastructure.Repositories;
internal class RestaurantRepository(RestaurantsDbContext context) : IRestaurantRepository
{
    public async Task<IEnumerable<Restaurant>> GetAllAsync()
    {
         var restaurants = await context.Restaurants.ToListAsync();
        return restaurants;
    }

    public async Task<Restaurant?> GetByIdAsync(Guid id)
    {
        var restaurant = await context.Restaurants.FirstOrDefaultAsync(r => r.Id == id);
        return restaurant;

    }
}
