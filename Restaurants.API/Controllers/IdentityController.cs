using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Users.Commands;

namespace Restaurants.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class IdentityController(IMediator mediator) : ControllerBase
{
    [HttpPatch("user-details")]
    public async Task<IActionResult> UpdateUserDetails(UpdateUserDetailsCommand updateUserCommand)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await mediator.Send(updateUserCommand);
        return NoContent();
    }
}
