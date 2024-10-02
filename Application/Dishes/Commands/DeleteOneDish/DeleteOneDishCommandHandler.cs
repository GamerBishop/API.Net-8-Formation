using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Dishes.Commands.DeleteOneDish;

public class DeleteOneDishCommandHandler(ILogger<DeleteOneDishCommandHandler> logger, 
    IRestaurantRepository restaurantRepository, 
    IDishRepository dishRepository, 
    IRestaurantAuthorizationService restaurantAuthorizationService) : IRequestHandler<DeleteOneDishCommand>
{
    public async Task Handle(DeleteOneDishCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Deleting dish with id {DishId} from restaurant with id {RestaurantGuid}", request.DishId, request.RestaurantGuid);

        var restaurant = await restaurantRepository.GetByIdAsync(request.RestaurantGuid) ?? throw new NotFoundException(nameof(Restaurant), request.RestaurantGuid.ToString());


        if (!restaurantAuthorizationService.Authorize(restaurant, ResourceOperation.Update))
        {
            logger.LogWarning("User not authorized to delete dish with id {DishId} from restaurant with id {RestaurantGuid}", request.DishId, request.RestaurantGuid);
            throw new ForbidException("User not authorized to delete dish from restaurant");
        }

        var dish = restaurant.Dishes.FirstOrDefault(d => d.Id == request.DishId) ?? throw new NotFoundException(nameof(Dish), request.DishId.ToString());

        await dishRepository.DeleteDishAsync(dish);
    }
}
