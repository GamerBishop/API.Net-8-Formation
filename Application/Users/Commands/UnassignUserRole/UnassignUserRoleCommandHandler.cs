using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;

namespace Restaurants.Application.Users.Commands.UnassignUserRole;

public class UnassignUserRoleCommandHandler(ILogger<UnassignUserRoleCommandHandler> logger,
        UserManager<User> userManager,
        RoleManager<IdentityRole> roleManager,
        IUserContext userContext)
    : IRequestHandler<UnassignUserRoleCommand>
{
    public async Task Handle(UnassignUserRoleCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Unassigning user role: {@Request}", request);

        CurrentUser cur = userContext.GetCurrentUser() ?? throw new InvalidOperationException("User context is not present.");

        // Find the user by email
        var user = await userManager.FindByEmailAsync(request.Email)
            ?? throw new NotFoundException(nameof(User), request.Email);

        var role = await roleManager.FindByNameAsync(request.Role)
            ?? throw new NotFoundException(nameof(IdentityRole), request.Role);

        // Remove the role from the user
        await userManager.RemoveFromRoleAsync(user, role.Name!);

        // Log succesful operations details and the CurrentUser that asked for the operation for security reasons
        logger.LogInformation("Role {Role} removed from user {Email} by {CurrentUser}", role.Name, user.Email, cur.ToString());
    }
}
