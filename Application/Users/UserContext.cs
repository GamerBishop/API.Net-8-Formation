using Microsoft.AspNetCore.Http;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Security.Claims;

namespace Restaurants.Application.Users;

public interface IUserContext
{
    CurrentUser? GetCurrentUser();
    Task<int> GetNumbersOfRestaurantsCreated();
}

public class UserContext(IHttpContextAccessor httpContextAccessor, IRestaurantRepository restaurantRepository) : IUserContext
{
    public CurrentUser? GetCurrentUser()
    {
        var user = httpContextAccessor?.HttpContext?.User ?? throw new InvalidOperationException("User context is not present.");

        if (user.Identity == null || !user.Identity.IsAuthenticated)
        {
            return null;
        }

        var userId = user.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        var email = user.FindFirst(ClaimTypes.Email)!.Value;
        var roles = user.FindAll(ClaimTypes.Role).Select(x => x.Value);
        var nationality = user.FindFirst(c => c.Type == "Nationality")?.Value;
        string birthDateString = user.FindFirst(c => c.Type == "BirthDate")?.Value ?? "";
        DateOnly? birthDate = string.IsNullOrEmpty(birthDateString) ? null : DateOnly.Parse(birthDateString,CultureInfo.InvariantCulture);

        return new CurrentUser(userId, email, roles, nationality, birthDate);
    }

    public async Task<int> GetNumbersOfRestaurantsCreated()
    {
        var user = httpContextAccessor?.HttpContext?.User ?? throw new InvalidOperationException("User context is not present.");

        if (user.Identity == null || !user.Identity.IsAuthenticated)
        {
            return 0;
        }
        var userId = user.FindFirst(ClaimTypes.NameIdentifier)!.Value;

        return await restaurantRepository.GetRestaurantsCountCreatedByUser(userId);
    }
}
