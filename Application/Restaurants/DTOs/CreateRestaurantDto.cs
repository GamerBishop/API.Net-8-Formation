using System.ComponentModel.DataAnnotations;

namespace Restaurants.Application.Restaurants.DTOs;

public class CreateRestaurantDto
{
    [StringLength(100, MinimumLength = 3)]
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    [Required(ErrorMessage = "Insert a valid category")]
    public string Category { get; set; } = default!;
    public bool HasDelivery { get; set; }

    [EmailAddress(ErrorMessage = "Insert a valid email")]
    public string? ContactEmail { get; set; }
    [Phone(ErrorMessage = "Insert a valid phone number")]
    public string? ContactNumber { get; set; }
    public string? City { get; set; }
    public string? Street { get; set; }
    [RegularExpression(@"^\d{2}-\d{3}$", ErrorMessage = "Insert a valid zip code (XX-XXX)")]
    public string? ZipCode { get; set; }
}
