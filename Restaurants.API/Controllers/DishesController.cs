using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Dishes.Commands.CreateDish;
using Restaurants.Application.Dishes.Commands.DeleteAllDishes;
using Restaurants.Application.Dishes.Commands.DeleteOneDish;
using Restaurants.Application.Dishes.Commands.UpdateDish;
using Restaurants.Application.Dishes.DTOs;
using Restaurants.Application.Dishes.Querys.GetAllDishes;
using Restaurants.Application.Dishes.Querys.GetAllDishesFromRestaurant;
using Restaurants.Application.Dishes.Querys.GetDishById;
using Restaurants.Infrastructure.Constants;

namespace Restaurants.API.Controllers
{
    /// <summary>
    /// Controller for managing dishes in a restaurant.
    /// </summary>
    [ApiController]
    [Route("api/restaurant/{restaurantGuid}/[controller]")]
    [Authorize]
    public class DishesController(IMediator _mediator) : ControllerBase
    {

        /// <summary>
        /// Gets all dishes from a restaurant.
        /// </summary>
        /// <param name="restaurantGuid">The restaurant GUID.</param>
        /// <returns>The list of dishes.</returns>
        [HttpGet]
        [Route("AllDishes")]
        [Authorize(Policy = PolicyNames.s_AtLeast20)]
        [ProducesResponseType(typeof(IEnumerable<DishDto>), 200)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllDishesFromRestaurant([FromRoute] Guid restaurantGuid)
        {
            var dishes = await _mediator.Send(new GetAllDishesFromRestaurantQuery(restaurantGuid));
            return Ok(dishes);
        }

        /// <summary>
        /// Gets a dish by its ID.
        /// </summary>
        /// <param name="restaurantGuid">The restaurant GUID.</param>
        /// <param name="id">The dish ID.</param>
        /// <returns>The dish.</returns>
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(DishDto), 200)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetDishById([FromRoute] Guid restaurantGuid, int id)
        {
            var dish = await _mediator.Send(new GetDishByIdQuery(id, restaurantGuid));
            return Ok(dish);
        }

        /// <summary>
        /// Creates a new dish for a restaurant.
        /// </summary>
        /// <param name="restaurantGuid">The restaurant GUID.</param>
        /// <param name="createDishCommand">The create dish command.</param>
        /// <returns>The created dish.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(int), 201)]
        public async Task<IActionResult> CreateDish([FromRoute] Guid restaurantGuid, CreateDishCommand createDishCommand)
        {
            createDishCommand.RestaurantId = restaurantGuid;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            int id = await _mediator.Send(createDishCommand);
            return CreatedAtAction(nameof(GetDishById), new { restaurantGuid, id }, null);
        }

        /// <summary>
        /// Updates a dish in a restaurant.
        /// </summary>
        /// <param name="restaurantGuid">The restaurant GUID.</param>
        /// <param name="Id">The dish ID.</param>
        /// <param name="updateDishCommand">The update dish command.</param>
        /// <returns>No content.</returns>
        [HttpPatch]
        [Route("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateDish([FromRoute] Guid restaurantGuid, [FromRoute] int Id, UpdateDishCommand updateDishCommand)
        {
            updateDishCommand.Id = Id;
            updateDishCommand.RestaurantGuid = restaurantGuid;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _mediator.Send(updateDishCommand);
            return NoContent();
        }

        /// <summary>
        /// Deletes a dish from a restaurant.
        /// </summary>
        /// <param name="restaurantGuid">The restaurant GUID.</param>
        /// <param name="id">The dish ID.</param>
        /// <returns>No content.</returns>
        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteDish([FromRoute] Guid restaurantGuid, [FromRoute] int id)
        {
            await _mediator.Send(new DeleteOneDishCommand(restaurantGuid, id));
            return NoContent();
        }

        /// <summary>
        /// Deletes all dishes from a restaurant.
        /// </summary>
        /// <param name="restaurantGuid">The restaurant GUID.</param>
        /// <returns>No content.</returns>
        [HttpDelete]
        [Route("DeleteAllDishes")]
        [ProducesResponseType(204)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAllDishes([FromRoute] Guid restaurantGuid)
        {
            await _mediator.Send(new DeleteAllDishesCommand(restaurantGuid));
            return NoContent();
        }
    }
}
