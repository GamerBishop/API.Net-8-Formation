using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Dishes.DTOs;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Dishes.Querys.GetAllDishesFromRestaurant;

public class GetAllDishesFromRestaurantQueryHandler(ILogger<GetAllDishesFromRestaurantQueryHandler> logger, IRestaurantRepository restaurantRepository, IDishRepository dishRepository, IMapper mapper) : IRequestHandler<GetAllDishesFromRestaurantQuery, IEnumerable<DishDto>>
{
    public async Task<IEnumerable<DishDto>> Handle(GetAllDishesFromRestaurantQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Retrieving all dishes from restaurant with id {RestaurantId}", request.RestaurantId);

        var restaurant = await restaurantRepository.GetByIdAsync(request.RestaurantId) ?? throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());

        var dishesDtos = mapper.Map<IEnumerable<DishDto>>(restaurant.Dishes);

        return dishesDtos;
    }
}
