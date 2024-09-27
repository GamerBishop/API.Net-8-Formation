using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.DTOs;
using Restaurants.Application.Restaurants.Querys.GetAllRestaurants;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Querys.GetRestaurantById;

public class GetRestaurantByIdQueryHandler(ILogger<GetRestaurantByIdQueryHandler> logger, IMapper mapper, IRestaurantRepository restaurantRepository) : IRequestHandler<GetRestaurantByIdQuery, RestaurantDto>
{
    public async Task<RestaurantDto> Handle(GetRestaurantByIdQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Retrieving restaurant with id {RestaurantId}", request.Id);
        var restaurant = await restaurantRepository.GetByIdAsync(request.Id);
        if (restaurant == null)
        {
            logger.LogWarning("Restaurant with id {RestaurantId} was not found", request.Id);
            throw new NotFoundException("Restaurant", request.Id.ToString());
        }
        else
        {
            logger.LogInformation("Retrieved restaurant with id {RestaurantId}", request.Id);
        }

        var restaurantDto = mapper.Map<RestaurantDto>(restaurant);

        return restaurantDto;
    }
}
