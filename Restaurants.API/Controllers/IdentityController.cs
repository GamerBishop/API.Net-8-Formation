using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Users.Commands.AssignUserRole;
using Restaurants.Application.Users.Commands.UnassignUserRole;
using Restaurants.Application.Users.Commands.UpdateUserDetails;

namespace Restaurants.API.Controllers;

/// <summary>
/// Controller for managing user identity.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class IdentityController(IMediator mediator) : ControllerBase
{
    /// <summary>
    /// Updates the details of a user.
    /// </summary>
    /// <param name="updateUserCommand">The command to update user details.</param>
    /// <returns>The result of the update operation.</returns>
    [HttpPatch("userDetails")]
    public async Task<IActionResult> UpdateUserDetails(UpdateUserDetailsCommand updateUserCommand)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await mediator.Send(updateUserCommand);
        return NoContent();
    }

    /// <summary>
    /// Assigns a role to a user.
    /// </summary>
    /// <param name="assignUserRoleCommand">The command to assign user role.</param>
    /// <returns>The result of the role assignment operation.</returns>
    [HttpPost("assignRole")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> AssignUserRole(AssignUserRoleCommand assignUserRoleCommand)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await mediator.Send(assignUserRoleCommand);
        return NoContent();
    }

    /* To easily check results
     * SELECT U.UserName, R.Name
     *    FROM [Restaurants].[dbo].[AspNetUsers] U
     *     Join Restaurants.dbo.AspNetUserRoles UR on U.Id = UR.UserId
     *    Join Restaurants.dbo.AspNetRoles R On UR.RoleId = R.Id
     */


    /*1. Créez une action dans le IdentityController :
    Réponds au verbe: HttpDelete
    Accessible uniquement aux Admins
    Paramètres :  Email & Nom du rôle
    2. Command & Handler pour enlever un rôle à un utilisateur
    3. Si un rôle ou un utilisateur n’existe pas, renvoyer un code 404 = NotFound, 
    204 - NoContent sinon*/
    [HttpDelete("unassignRole")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UnassignUserRole(UnassignUserRoleCommand unassignUserRoleCommand)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await mediator.Send(unassignUserRoleCommand);
        return NoContent();
    }
}
