using MediatR;
using Restaurants.Application.Dishes.DTOs;

namespace Restaurants.Application.Dishes.Querys.GetDishById;

public class GetDishByIdQuery(int id, Guid restaurantId) : IRequest<DishDto>
{
    public int Id { get; } = id;
    public Guid RestaurantId { get; } = restaurantId;
}
