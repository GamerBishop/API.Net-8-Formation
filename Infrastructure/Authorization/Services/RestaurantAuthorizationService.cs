using Microsoft.Extensions.Logging;
using Restaurants.Application.Users;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Interfaces;

namespace Restaurants.Infrastructure.Authorization.Services;

public class RestaurantAuthorizationService(ILogger<RestaurantAuthorizationService> logger, IUserContext userContext) : IRestaurantAuthorizationService
{
    public bool Authorize(Restaurant restaurant, ResourceOperation operation)
    {
        var user = userContext.GetCurrentUser() ?? throw new UnauthorizedAccessException("User not authenticated");

        logger.LogInformation("Checking if user {UserEmail} [{UserId}] can {Operation} restaurant {RestaurantName}", user.Email, user.Id, operation, restaurant.Name);

        if (operation == ResourceOperation.Create || operation == ResourceOperation.Read)
        {
            logger.LogInformation("Create/REad Operation - Successful authorization");
            return true;
        }

        if (operation == ResourceOperation.Delete && user.IsEnroledIn(UserRoles.Admin.Name))
        {
            logger.LogInformation("Delete Operation - Successful authorization for Admin");
            return true;
        }

        if ((operation == ResourceOperation.Update || user.IsEnroledIn(UserRoles.Owner.Name)) && restaurant.OwnerId == user.Id)
        {
            logger.LogInformation("Update/Delete Operation - Successful authorization for Owner");
            return true;
        }

        return false;

    }
}
