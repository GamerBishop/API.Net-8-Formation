using MediatR;

namespace Restaurants.Application.Dishes.Commands.DeleteAllDishes;

public class DeleteAllDishesCommand(Guid restaurantGuid) : IRequest
{
    public Guid RestaurantGuid { get; } = restaurantGuid;
}
