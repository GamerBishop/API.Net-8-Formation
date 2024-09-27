using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Commands.UpdateRestaurant;

public class UpdateRestaurantCommandHandler(ILogger<UpdateRestaurantCommandHandler> _logger, IRestaurantRepository restaurantRepository, IMapper mapper ) : IRequestHandler<UpdateRestaurantCommand>
{
    public async Task Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating restaurant with id : {RestaurantId} with {@UpdatedRestaurant}", request.Id, request);
        
        var restaurant = await restaurantRepository.GetByIdAsync(request.Id);
        if (restaurant == null)
        {
            _logger.LogWarning($"Restaurant with id {request.Id} not found");
            throw new NotFoundException("Restaurant", request.Id.ToString());
        }

        mapper.Map(request, restaurant);

        await restaurantRepository.UpdateAsync(restaurant);
    }
}
