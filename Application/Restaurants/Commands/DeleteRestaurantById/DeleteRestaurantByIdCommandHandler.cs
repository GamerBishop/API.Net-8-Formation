using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Commands.DeleteRestaurantById;

public class DeleteRestaurantByIdCommandHandler(ILogger<DeleteRestaurantByIdCommandHandler> logger, IRestaurantRepository repository) : IRequestHandler<DeleteRestaurantByIdCommand>
{
    public async Task  Handle(DeleteRestaurantByIdCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Deleting restaurant with id {RestaurantId}", request.Id);

        //Delete restaurant logic
        var restaurant = await repository.GetByIdAsync(request.Id);
        if (restaurant == null)
        {
            logger.LogWarning("Restaurant with id {requestId} not found", request.Id);
            throw new NotFoundException("Restaurant", request.Id.ToString());
        }

        await repository.DeleteAsync(restaurant);
    }
}
