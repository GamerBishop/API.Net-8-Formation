using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Dishes.DTOs;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Dishes.Querys.GetAllDishes;

public class GetAllDishesQueryHandler(ILogger<GetAllDishesQueryHandler> logger, IDishRepository dishRepository, IMapper mapper) : IRequestHandler<GetAllDishesQuery, IEnumerable<DishDto>>
{
    public async Task<IEnumerable<DishDto>> Handle(GetAllDishesQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Retrieving all dishes");
        var dishes = await dishRepository.GetAllDishesAsync();
        logger.LogInformation("Retrieved {DishCount} dishes", dishes.Count());

        var dishesDtos = mapper.Map<IEnumerable<DishDto>>(dishes);

        return dishesDtos;

    }
}
