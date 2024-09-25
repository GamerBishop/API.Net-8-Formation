using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Commands.UpdateRestaurant;

public class UpdateRestaurantCommandHandler(ILogger<UpdateRestaurantCommandHandler> _logger, IRestaurantRepository restaurantRepository ) : IRequestHandler<UpdateRestaurantCommand, Boolean>
{
    public async Task<Boolean> Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Updating restaurant with id {request.Id}");
        // Update restaurant logic
        if (request == null)
        {
            _logger.LogWarning("UpdateRestaurantCommand is null");
            return false;
        }

        var restaurant = await restaurantRepository.GetByIdAsync(request.Id);
        if (restaurant == null)
        {
            _logger.LogWarning($"Restaurant with id {request.Id} not found");
            return false;
        }

        restaurant.Name = request.Name;
        restaurant.Description = request.Description;
        restaurant.HasDelivery = request.HasDelivery == true ? true : false;

        var isUpdated = await restaurantRepository.UpdateAsync(restaurant);


        return isUpdated;
    }
}
