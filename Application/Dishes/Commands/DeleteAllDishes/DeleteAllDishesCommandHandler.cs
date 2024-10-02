using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Dishes.Commands.DeleteAllDishes;

public class DeleteAllDishesCommandHandler(ILogger<DeleteAllDishesCommandHandler> logger, 
    IRestaurantRepository restaurantRepository, 
    IDishRepository dishRepository,
    IRestaurantAuthorizationService restaurantAuthorizationService) : IRequestHandler<DeleteAllDishesCommand>
{
    public async Task Handle(DeleteAllDishesCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Deleting all dishes from restaurant with id {RestaurantGuid}", request.RestaurantGuid);

        var restaurant = await restaurantRepository.GetByIdAsync(request.RestaurantGuid) ?? throw new NotFoundException(nameof(Restaurant), request.RestaurantGuid.ToString());

        if (!restaurantAuthorizationService.Authorize(restaurant, ResourceOperation.Update))
        {
            logger.LogWarning("User not authorized to delete dishes from restaurant with id {RestaurantGuid}", request.RestaurantGuid);
            throw new ForbidException("User not authorized to delete dishes from restaurant");
        }

        await dishRepository.DeleteDishesAsync(restaurant.Dishes);        
    }
}
