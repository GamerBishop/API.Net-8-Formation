using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Dishes.Commands.CreateDish;

public class CreateDishCommandHandler(ILogger<CreateDishCommandHandler> logger, 
    IRestaurantRepository restaurantRepository, 
    IDishRepository dishRepository, 
    IMapper mapper,
    IRestaurantAuthorizationService restaurantAuthorizationService) : IRequestHandler<CreateDishCommand, int>
{
    public async Task<int> Handle(CreateDishCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Creating dish with name {@Dish}", request);

        var restaurant = await restaurantRepository.GetByIdAsync(request.RestaurantId) ?? throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());
        var dish = mapper.Map<Dish>(request);

        if (!restaurantAuthorizationService.Authorize(restaurant, ResourceOperation.Update))
        {
            logger.LogWarning("User not authorized to create dish for restaurant with id {RestaurantId}", request.RestaurantId);
            throw new ForbidException("User not authorized to create dish for restaurant");
        }

        int createdId = await dishRepository.CreateDishAsync(dish);

        return createdId;
    }
}
