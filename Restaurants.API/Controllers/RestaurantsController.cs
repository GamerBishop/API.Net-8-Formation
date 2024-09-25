
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Restaurants;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Application.Restaurants.Commands.DeleteRestaurantById;
using Restaurants.Application.Restaurants.Commands.UpdateRestaurant;
using Restaurants.Application.Restaurants.DTOs;
using Restaurants.Application.Restaurants.Querys.GetAllRestaurants;
using Restaurants.Application.Restaurants.Querys.GetRestaurantById;

namespace Restaurants.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RestaurantsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var restaurants = await mediator.Send(new GetAllRestaurantsQuery());
        return Ok(restaurants);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute]Guid id){
        var restaurant = await mediator.Send(new GetRestaurantByIdQuery(id));
        if (restaurant == null)
        {
            return NotFound();
        }
        return Ok(restaurant);
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create(CreateRestaurantCommand createRestaurantCommand)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        Guid id = await mediator.Send(createRestaurantCommand);
        return CreatedAtAction(nameof(GetById), new { id }, null);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var isDeleted = await mediator.Send(new DeleteRestaurantByIdCommand(id));

        if (!isDeleted)
            return NotFound();

        return NoContent();
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, UpdateRestaurantCommand updateRestaurantCommand)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        updateRestaurantCommand.Id = id;
        await mediator.Send(updateRestaurantCommand);

        return NoContent();
    }
}
