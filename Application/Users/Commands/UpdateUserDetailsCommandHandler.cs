using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Users.Commands;
using Restaurants.Application.Users;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;

namespace Restaurants.Application.Users.Commands;
public class UpdateUserDetailsCommandHandler(ILogger<UpdateUserDetailsCommandHandler> logger,
    IUserContext userContext,
    IUserStore<User> userStore) : IRequestHandler<UpdateUserDetailsCommand>
{
    public async Task Handle(UpdateUserDetailsCommand request, CancellationToken cancellationToken)
    {
        var user = userContext.GetCurrentUser();
        logger.LogInformation("Updating user: {UserId}, with {@Request}", user!.Id, request);

        User dbUser = await userStore.FindByIdAsync(user!.Id, cancellationToken) ?? throw new NotFoundException(nameof(User), user!.Id.ToString());
        
        dbUser.Nationality = request.Nationality;
        dbUser.BirthDate = request.BirthDate;
        
        await userStore.UpdateAsync(dbUser, cancellationToken);
    }
}