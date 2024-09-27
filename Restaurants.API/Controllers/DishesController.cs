﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Dishes.Commands.CreateDish;
using Restaurants.Application.Dishes.Commands.DeleteAllDishes;
using Restaurants.Application.Dishes.Commands.DeleteOneDish;
using Restaurants.Application.Dishes.Commands.UpdateDish;
using Restaurants.Application.Dishes.Querys.GetAllDishes;
using Restaurants.Application.Dishes.Querys.GetAllDishesFromRestaurant;
using Restaurants.Application.Dishes.Querys.GetDishById;

namespace Restaurants.API.Controllers
{
    [ApiController]
    [Route("api/restaurant/{restaurantGuid}/[controller]")]
    [Authorize]
    public class DishesController(IMediator mediator) : ControllerBase
    {

        [HttpGet]
        [Route("AllDishes")]
        public async Task<IActionResult> GetAllDishesFromRestaurant([FromRoute]Guid restaurantGuid)
        {
            var dishes = await mediator.Send(new GetAllDishesFromRestaurantQuery(restaurantGuid));
            return Ok(dishes);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetDishById([FromRoute] Guid restaurantGuid, int id)
        {
            var dish = await mediator.Send(new GetDishByIdQuery(id, restaurantGuid));
            return Ok(dish);
        }

        [HttpPost]
        public async Task<IActionResult> CreateDish([FromRoute] Guid restaurantGuid, CreateDishCommand createDishCommand)
        {
            createDishCommand.RestaurantId = restaurantGuid;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            int id = await mediator.Send(createDishCommand);
            return CreatedAtAction(nameof(GetDishById), new { restaurantGuid, id }, null);
        }

        [HttpPatch]
        [Route("{id}")]
        public async Task<IActionResult> UpdateDish([FromRoute] Guid restaurantGuid, [FromRoute] int Id, UpdateDishCommand updateDishCommand)
        {
            updateDishCommand.Id = Id;
            updateDishCommand.RestaurantGuid = restaurantGuid;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await mediator.Send(updateDishCommand);
            return NoContent();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteDish([FromRoute] Guid restaurantGuid, [FromRoute] int id)
        {
            await mediator.Send(new DeleteOneDishCommand(restaurantGuid,id));
            return NoContent();
        }

        [HttpDelete]
        [Route("DeleteAllDishes")]
        public async Task<IActionResult> DeleteAllDishes([FromRoute] Guid restaurantGuid)
        {
            await mediator.Send(new DeleteAllDishesCommand(restaurantGuid));
            return NoContent();
        }
    }
}
