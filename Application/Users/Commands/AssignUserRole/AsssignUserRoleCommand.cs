using MediatR;

namespace Restaurants.Application.Users.Commands.AssignUserRole;

public class AssignUserRoleCommand : IRequest
{
    public required string UserEmail { get; set; }
    public required string Role { get; set; }
}