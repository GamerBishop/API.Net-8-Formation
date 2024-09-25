using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Commands.DeleteRestaurantById;

public class DeleteRestaurantByIdCommandHandler(ILogger<DeleteRestaurantByIdCommandHandler> logger, IRestaurantRepository repository) : IRequestHandler<DeleteRestaurantByIdCommand, Boolean>
{
    public async Task<Boolean>  Handle(DeleteRestaurantByIdCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Deleting restaurant with id {RestaurantId}", request.Id);

        //Delete restaurant logic
        var restaurant = await repository.GetByIdAsync(request.Id);
        if (restaurant == null)
        {
            logger.LogWarning($"Restaurant with id {request.Id} not found");
            return false;
        }

        Boolean isDeleted = await repository.DeleteAsync(restaurant);

        return isDeleted;
    }
}
