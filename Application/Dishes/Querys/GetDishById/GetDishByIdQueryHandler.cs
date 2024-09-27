using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Dishes.DTOs;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Dishes.Querys.GetDishById;

public class GetDishByIdQueryHandler(ILogger<GetDishByIdQueryHandler> logger, IRestaurantRepository restaurantRepository, IMapper mapper) : IRequestHandler<GetDishByIdQuery, DishDto>
{
    public async Task<DishDto> Handle(GetDishByIdQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Retrieving dish with id {Id}", request.Id);
     
        var restaurant = await restaurantRepository.GetByIdAsync(request.RestaurantId) ?? throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());

        var result = restaurant.Dishes.FirstOrDefault(d => d.Id == request.Id) ?? throw new NotFoundException(nameof(Dish), request.Id.ToString());
        
        var dishDto = mapper.Map<DishDto>(result);

        return dishDto;
    }
}
