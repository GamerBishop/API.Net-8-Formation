using MediatR;
using Restaurants.Application.Restaurants.DTOs;

namespace Restaurants.Application.Restaurants.Querys.GetRestaurantById;

public class GetRestaurantByIdQuery(Guid id) : IRequest<RestaurantDto?>
{
    public Guid Id { get; } = id;
    
    
    //OU public Guid Id { get; init; } avec un constructeur
}
