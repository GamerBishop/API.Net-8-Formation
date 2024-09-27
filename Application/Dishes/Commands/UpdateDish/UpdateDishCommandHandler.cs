using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Dishes.Commands.UpdateDish;

public class UpdateDishCommandHandler(ILogger<UpdateDishCommandHandler> logger, IDishRepository dishRepository, IRestaurantRepository restaurantRepository, IMapper mapper) : IRequestHandler<UpdateDishCommand>
{
    public async Task Handle(UpdateDishCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Updating dish with id {Id} in restaurant with id {restaurantId}", request.Id, request.RestaurantGuid);

        var restaurant = await restaurantRepository.GetByIdAsync(request.RestaurantGuid) ?? throw new NotFoundException(nameof(Restaurant), request.RestaurantGuid.ToString());

        var dish = restaurant.Dishes.FirstOrDefault(d => d.Id == request.Id) ?? throw new NotFoundException(nameof(Dish), request.Id.ToString());

        mapper.Map(request, dish);

        await dishRepository.UpdateDishAsync(dish);
    }
}
