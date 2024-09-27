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
        logger.LogInformation("Updating dish with id {Id} in restaurant with id {restaurantId}", request.Id, request.RestaurantId);

        var restaurant = await restaurantRepository.GetByIdAsync(request.RestaurantId) ?? throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());

        var dish = await dishRepository.GetDishByIdAsync(request.Id) ?? throw new NotFoundException(nameof(Dish), request.Id.ToString());

        mapper.Map(request, dish);

        await dishRepository.UpdateDishAsync(dish);
    }
}
