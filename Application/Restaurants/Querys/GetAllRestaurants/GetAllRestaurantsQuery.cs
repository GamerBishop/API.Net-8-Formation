using MediatR;
using Restaurants.Application.Common;
using Restaurants.Application.Restaurants.DTOs;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.Restaurants.Querys.GetAllRestaurants;

public class GetAllRestaurantsQuery : IRequest<PagedResult<RestaurantDto>>
{
    public string? SearchPhrase { get; set; }
    public int PageSize { get; set; }
    public int PageNumber { get; set; }
}
