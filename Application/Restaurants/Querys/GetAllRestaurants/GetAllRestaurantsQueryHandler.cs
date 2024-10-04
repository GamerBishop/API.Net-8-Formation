using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.DTOs;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using static System.Formats.Asn1.AsnWriter;
using System.Xml.Linq;
using Restaurants.Application.Common;

namespace Restaurants.Application.Restaurants.Querys.GetAllRestaurants;

public class GetAllRestaurantsQueryHandler(ILogger<GetAllRestaurantsQueryHandler> logger, IMapper mapper, IRestaurantRepository restaurantRepository) : IRequestHandler<GetAllRestaurantsQuery, PagedResult<RestaurantDto>>
{
    public async Task<PagedResult<RestaurantDto>> Handle(GetAllRestaurantsQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Retrieving all restaurants");

        var restaurants = await restaurantRepository.GetAllMatchingAsync(request.SearchPhrase,
                                                                         request.PageSize,
                                                                         request.PageNumber,
                                                                         request.SortBy,
                                                                         request.SortDirection);
        

        logger.LogInformation("Retrieved {RestaurantCount} restaurants", restaurants.TotalCount);
        var restaurantsDtos = mapper.Map<IEnumerable<RestaurantDto>>(restaurants.Restaurants);

        return new PagedResult<RestaurantDto>(restaurantsDtos, restaurants.TotalCount,request.PageSize,request.PageNumber);
    }
}
