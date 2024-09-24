using FluentValidation;
using Restaurants.Application.Restaurants.DTOs;

namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant;

public class CreateRestaurantCommandValidator : AbstractValidator<CreateRestaurantCommand>
{
    private readonly List<string> p_ValidCategories = ["Fast Food", "Traditional", "Italian", "Chinese", "Indian", "Mexican", "American", "Other", "French"];

    public CreateRestaurantCommandValidator()
    {
        RuleFor(dto => dto.Name)
            .NotEmpty()
            .Length(3, 100);

        RuleFor(dto => dto.Description)
            .NotEmpty()
            .WithMessage("Description is required");

        RuleFor(dto => dto.City)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(dto => dto.Street)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(dto => dto.ZipCode)
            .NotEmpty()
            .Matches(@"^\d{2}-\d{3}$")
            .WithMessage("Insert a valid zip code (XX-XXX)");

        RuleFor(dto => dto.Category)
            .NotEmpty()
            .MaximumLength(100)
            .Must(p_ValidCategories.Contains)
            .WithMessage("Insert a valid category");

        RuleFor(dto => dto.ContactEmail)
            .EmailAddress()
            .When(dto => dto.ContactEmail != null)
            .WithMessage("Please provide valid email adress");

        RuleFor(dto => dto.ContactNumber)
            .Matches(@"^\d{10}$")
            .WithMessage("Insert a valid phone number");



    }
}
