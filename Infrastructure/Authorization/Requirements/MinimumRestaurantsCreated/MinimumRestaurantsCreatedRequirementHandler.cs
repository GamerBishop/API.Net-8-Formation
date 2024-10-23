using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Users;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Authorization.Requirements.MinimumAge;

namespace Restaurants.Infrastructure.Authorization.Requirements.MinimumRestaurantsCreated;

public class MinimumRestaurantsCreatedRequirementHandler(ILogger<MinimumAgeRequirementHandler> logger, IUserContext userContext, IRestaurantRepository restaurantRepository) : AuthorizationHandler<MinimumRestaurantsCreatedRequirement>
{
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumRestaurantsCreatedRequirement requirement)
    {
        var currentUser = userContext.GetCurrentUser() ?? throw new InvalidOperationException("User context is not present.");
        int restaurantsCreated = await restaurantRepository.GetRestaurantsCountCreatedByUser(currentUser.Id);

        logger.LogInformation("User : {Email} has created {RestaurantsCreated} restaurants - Handling MinimumRestaurantsCreatedRequirement with value {MinimumRestaurantsCreated}", currentUser.Email, restaurantsCreated, requirement.MinimumRestaurantsCreated);

        if (restaurantsCreated >= requirement.MinimumRestaurantsCreated)
        {
            logger.LogInformation("Authorization suceeded.");
            context.Succeed(requirement);
        }
        else
        {
            logger.LogInformation("Authorization failed.");
            context.Fail();
        }
    }
}
