using FluentValidation;

namespace Restaurants.Application.Users.Commands.UnassignUserRole;

public class UnassignUserRoleCommandValidator : AbstractValidator<UnassignUserRoleCommand>
{
    public UnassignUserRoleCommandValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress().WithErrorCode("400").WithMessage("Email is required.");
        RuleFor(x => x.Role).NotEmpty().WithErrorCode("400").WithMessage("Role is required.");
    }
}
