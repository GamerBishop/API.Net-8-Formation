using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Persistence;
using System.Linq.Expressions;

namespace Restaurants.Infrastructure.Repositories;
internal class RestaurantRepository(RestaurantsDbContext context) : IRestaurantRepository
{
    public async Task<IEnumerable<Restaurant>> GetAllAsync()
    {
         var restaurants = await context.Restaurants.Include(r => r.Dishes).ToListAsync();
        return restaurants;
    }

    public async Task<(IEnumerable<Restaurant>, int)> GetAllMatchingAsync(string? _searchPhrase, int PageSize, int PageNumber, string? SortBy, SortDirection? sortDirection)
    {
        var lowerCaseSearchPhrase = _searchPhrase?.ToLower();

        var baseQuery = context.Restaurants
                                .Where(r => string.IsNullOrEmpty(lowerCaseSearchPhrase) || (r.Name.Contains(lowerCaseSearchPhrase, StringComparison.CurrentCultureIgnoreCase) ||
                                                                        r.Description.Contains(lowerCaseSearchPhrase, StringComparison.CurrentCultureIgnoreCase)));

        var totalCount = await baseQuery.CountAsync();

        if (!string.IsNullOrEmpty(SortBy))
        {
            var columnSelector = new Dictionary<string, Expression<Func<Restaurant, object?>>>
            {
                { nameof(Restaurant.Name), r => r.Name },
                { nameof(Restaurant.Category), r => r.Category },
                { nameof(Restaurant.Adress.City), r => r.Adress!.City },
                { nameof(Restaurant.Description), r => r.Description },
                { nameof(Restaurant.Adress.ZipCode), r => r.Adress!.ZipCode }
            };
            var selectedColumn = columnSelector[SortBy];

            baseQuery = sortDirection == SortDirection.Ascending 
                ? baseQuery.OrderBy(selectedColumn)
                : baseQuery.OrderByDescending(selectedColumn);
        }

        var restaurants = await baseQuery
            .Include(r => r.Dishes)
            .Skip((PageNumber - 1) * PageSize)
            .Take(PageSize)
            .ToListAsync();

        return (restaurants, totalCount);
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
