
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Dishes.Querys.GetAllDishes;
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
    //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<RestaurantDto>))]
    public async Task<ActionResult<IEnumerable<RestaurantDto>>> GetAll()
    {
        var restaurants = await mediator.Send(new GetAllRestaurantsQuery());
        return Ok(restaurants);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<RestaurantDto?>> GetById([FromRoute]Guid id){
        var restaurant = await mediator.Send(new GetRestaurantByIdQuery(id));
        return Ok(restaurant);
    }

    [HttpPost("create")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        await mediator.Send(new DeleteRestaurantByIdCommand(id));
        return NoContent();
    }

    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
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

    [HttpGet]
    [Route("dishes")]
    public async Task<IActionResult> GetAllDishes()
    {
        var dishes = await mediator.Send(new GetAllDishesQuery());
        return Ok(dishes);
    }
}
