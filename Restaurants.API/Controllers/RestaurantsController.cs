
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Dishes.DTOs;
using Restaurants.Application.Dishes.Querys.GetAllDishes;
using Restaurants.Application.Restaurants;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Application.Restaurants.Commands.DeleteRestaurantById;
using Restaurants.Application.Restaurants.Commands.UpdateRestaurant;
using Restaurants.Application.Restaurants.DTOs;
using Restaurants.Application.Restaurants.Querys.GetAllRestaurants;
using Restaurants.Application.Restaurants.Querys.GetRestaurantById;

namespace Restaurants.API.Controllers;

/// <summary>
/// Controller for managing restaurants.
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class RestaurantsController(IMediator mediator) : ControllerBase
{
    /// <summary>
    /// Get all restaurants.
    /// </summary>
    /// <returns>A list of restaurant DTOs.</returns>
    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType(200, Type = typeof(IEnumerable<RestaurantDto>))]
    public async Task<ActionResult<IEnumerable<RestaurantDto>>> GetAll()
    {
        var restaurants = await mediator.Send(new GetAllRestaurantsQuery());
        return Ok(restaurants);
    }

    /// <summary>
    /// Get a restaurant by its ID.
    /// </summary>
    /// <param name="id">The ID of the restaurant.</param>
    /// <returns>The restaurant DTO.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(200, Type = typeof(RestaurantDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<RestaurantDto?>> GetById([FromRoute] Guid id)
    {
        var restaurant = await mediator.Send(new GetRestaurantByIdQuery(id));
        return Ok(restaurant);
    }

    /// <summary>
    /// Create a new restaurant.
    /// </summary>
    /// <param name="createRestaurantCommand">The command to create a restaurant.</param>
    /// <returns>The created restaurant's ID.</returns>
    [HttpPost("create")]
    [Authorize(Roles = "Owner")]
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

    /// <summary>
    /// Delete a restaurant by its ID.
    /// </summary>
    /// <param name="id">The ID of the restaurant to delete.</param>
    /// <returns>No content.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        await mediator.Send(new DeleteRestaurantByIdCommand(id));
        return NoContent();
    }

    /// <summary>
    /// Update a restaurant by its ID.
    /// </summary>
    /// <param name="id">The ID of the restaurant to update.</param>
    /// <param name="updateRestaurantCommand">The command to update the restaurant.</param>
    /// <returns>No content.</returns>
    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update([FromRoute] Guid id, UpdateRestaurantCommand updateRestaurantCommand)
    {
        updateRestaurantCommand.Id = id;

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        await mediator.Send(updateRestaurantCommand);

        return NoContent();
    }

    /// <summary>
    /// Get all dishes.
    /// </summary>
    /// <returns>A list of dish DTOs.</returns>
    [HttpGet]
    [Route("dishes")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<DishDto>))]
    public async Task<IActionResult> GetAllDishes()
    {
        var dishes = await mediator.Send(new GetAllDishesQuery());
        return Ok(dishes);
    }
}
