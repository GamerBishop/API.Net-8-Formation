using MediatR;
using Restaurants.Application.Dishes.DTOs;

namespace Restaurants.Application.Dishes.Querys.GetAllDishesFromRestaurant;

public class GetAllDishesFromRestaurantQuery(Guid restaurantGuid) : IRequest<IEnumerable<DishDto>>
{
    public Guid RestaurantId { get; } = restaurantGuid;
}
