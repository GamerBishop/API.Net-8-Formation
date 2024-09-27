using MediatR;

namespace Restaurants.Application.Dishes.Commands.UpdateDish;

public class UpdateDishCommand(int Id, Guid restaurantGuid) : IRequest
{
    public int Id { get; set; } = Id;
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public decimal Price { get; set; }
    public bool IsVegetarian { get; set; }
    public int? KiloCalories { get; set; }
    public Guid RestaurantGuid { get; set; } = restaurantGuid;
}
