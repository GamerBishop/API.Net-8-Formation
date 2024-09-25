using MediatR;

namespace Restaurants.Application.Restaurants.Commands.DeleteRestaurantById;

public class DeleteRestaurantByIdCommand(Guid id) : IRequest<Boolean>
{
    public Guid Id { get; } = id;
}
