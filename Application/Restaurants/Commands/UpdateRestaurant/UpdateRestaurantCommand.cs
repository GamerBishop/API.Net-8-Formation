using MediatR;

namespace Restaurants.Application.Restaurants.Commands.UpdateRestaurant;

public class UpdateRestaurantCommand(Guid ID) : IRequest
{
    public Guid Id { get; set; } = ID;
    public String Name { get; set; } = default!;
    public String Description { get; set; } = default!;
    public Boolean? HasDelivery { get; set; }
}
