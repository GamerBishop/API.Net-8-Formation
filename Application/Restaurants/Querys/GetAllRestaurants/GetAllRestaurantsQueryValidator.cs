using FluentValidation;
using Restaurants.Application.Restaurants.DTOs;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.Restaurants.Querys.GetAllRestaurants;

public class GetAllRestaurantsQueryValidator : AbstractValidator<GetAllRestaurantsQuery>
{
    private readonly int[] p_AllowedPageSizes = [ 5, 10, 15, 20, 25, 30, 50, 100 ];
    private readonly string[] p_AllowedSortByColumnNames = [ nameof(RestaurantDto.Name), nameof(RestaurantDto.Category), nameof(RestaurantDto.City), nameof(RestaurantDto.Description), nameof(RestaurantDto.ZipCode) ];
    public GetAllRestaurantsQueryValidator()
    {
        RuleFor(x => x.PageSize)
            .GreaterThan(0)
            .WithMessage("Page size must be greater than 0")
            .Must(ps => p_AllowedPageSizes.Contains(ps))
            .WithMessage($"Page size must be one of the following values: {string.Join(", ", p_AllowedPageSizes)}");

        RuleFor(x => x.PageNumber)
            .GreaterThan(0)
            .WithMessage("Page number must be greater than 0");

        RuleFor(x => x.SortBy)
            .Must(sb => string.IsNullOrEmpty(sb) || p_AllowedSortByColumnNames.Contains(sb))
            .WithMessage($"Sort by column name must be one of the following values: {string.Join(", ", p_AllowedSortByColumnNames)}");
    }
}
