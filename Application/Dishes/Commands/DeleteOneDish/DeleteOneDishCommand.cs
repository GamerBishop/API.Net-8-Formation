using MediatR;

namespace Restaurants.Application.Dishes.Commands.DeleteOneDish;

public class DeleteOneDishCommand(Guid restaurantGuid, int dishId) : IRequest
{
    public Guid RestaurantGuid { get; } = restaurantGuid;
    public int DishId { get; } = dishId;
}