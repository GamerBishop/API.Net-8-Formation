using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Commands.UpdateRestaurant;

public class UpdateRestaurantCommandHandler(ILogger<UpdateRestaurantCommandHandler> _logger, 
    IRestaurantRepository restaurantRepository, 
    IMapper mapper,
    IRestaurantAuthorizationService restaurantAuthorizationService) : IRequestHandler<UpdateRestaurantCommand>
{
    public async Task Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating restaurant with id : {RestaurantId} with {@UpdatedRestaurant}", request.Id, request);

        var restaurant = await restaurantRepository.GetByIdAsync(request.Id);
        if (restaurant == null)
        {
            _logger.LogWarning("Restaurant with id {RestaurantId} not found", request.Id);
            throw new NotFoundException("Restaurant", request.Id.ToString());
        }

        if (!restaurantAuthorizationService.Authorize(restaurant, ResourceOperation.Update))
        {
            _logger.LogWarning("User not authorized to update restaurant with id {requestId}", request.Id);
            throw new ForbidException("User not authorized to update restaurant");
        }

        mapper.Map(request, restaurant);

        await restaurantRepository.UpdateAsync(restaurant);
    }
}
