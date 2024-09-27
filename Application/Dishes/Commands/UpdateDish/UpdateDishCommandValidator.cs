using FluentValidation;

namespace Restaurants.Application.Dishes.Commands.UpdateDish
{
    public class UpdateDishCommandValidator : AbstractValidator<UpdateDishCommand>
    {
        public UpdateDishCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithErrorCode("1001").WithMessage("The name of the dish is required.");
            RuleFor(x => x.Description).NotEmpty().WithErrorCode("1002").WithMessage("The description of the dish is required.");
            RuleFor(x => x.Price).GreaterThanOrEqualTo(0).WithErrorCode("1003").WithMessage("The price of the dish must be greater than or equal to 0.");
            RuleFor(x => x.KiloCalories).GreaterThan(0).WithErrorCode("1004").WithMessage("The number of kilocalories in the dish must be greater than 0.");
            RuleFor(x => x.IsVegetarian).NotNull().WithErrorCode("1005").WithMessage("The vegetarian status of the dish is required.");
            RuleFor(x => x.RestaurantId).NotEmpty().WithErrorCode("1006").WithMessage("The restaurant ID is required.");
            RuleFor(x => x.Id).NotEmpty().WithErrorCode("1007").WithMessage("The dish ID is required.");
        }
    }
}
