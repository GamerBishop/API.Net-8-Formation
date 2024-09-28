using FluentValidation;

namespace Restaurants.Application.Users.Commands.AssignUserRole;

public class AssignUserRoleCommandValidator : AbstractValidator<AssignUserRoleCommand>
{
    public AssignUserRoleCommandValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress().WithErrorCode("400").WithMessage("Email is required.");
        RuleFor(x => x.Role).NotEmpty().WithErrorCode("400").WithMessage("Role is required.");
    }
}