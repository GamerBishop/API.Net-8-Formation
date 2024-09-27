using MediatR;

namespace Restaurants.Application.Users.Commands;

public class UpdateUserDetailsCommand : IRequest
{
    public required DateOnly BirthDate { get; set; }
    public required string Nationality { get; set; }
}
