using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Users;

namespace Restaurants.Infrastructure.Authorization.Requirements;

public class MinimumAgeRequirementHandler(ILogger<MinimumAgeRequirementHandler> logger, IUserContext userContext) : AuthorizationHandler<MinimumAgeRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumAgeRequirement requirement)
    {
        var currentUser = userContext.GetCurrentUser() ?? throw new InvalidOperationException("User context is not present.");

        logger.LogInformation("User : {Email} date of birth is {BirthDate} - Handling MinimumAgeRequirement with value {MinimumAge}", currentUser.Email, currentUser.BirthDate, requirement.MinimumAge);

        if (currentUser.BirthDate == null)
        {
            logger.LogInformation("Authorization failed. - BirthDate is null.");
            context.Fail();
            return Task.CompletedTask;
        }

        // Checks if UserAge is greater than the minimum age requirement
        if (currentUser.BirthDate.Value.AddYears(requirement.MinimumAge) <= DateOnly.FromDateTime(DateTime.Now))
        {
            logger.LogInformation("Authorization suceeded.");
            context.Succeed(requirement);
        }
        else
        {
            context.Fail();
        }

        return Task.CompletedTask;
    }
}
