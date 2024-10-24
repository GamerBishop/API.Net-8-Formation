using AutoMapper;
using FluentAssertions;
using Restaurants.Application.Dishes.DTOs;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.Restaurants.DTOs.Tests;

public class RestaurantProfileTests
{
    private readonly Mapper p_Mapper;


    public RestaurantProfileTests()
    { 
        // Arrange
        var configuration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<RestaurantProfile>();
            cfg.AddProfile<DishesProfile>();
        }); 
        p_Mapper = new Mapper(configuration);
    }
    [Fact()]
    public void CreateMap_ForRestaurantToRestaurantDto_ShouldMapCorrectly()
    {
        // Arrange 
        var guid = Guid.NewGuid();

        var restaurant = new Restaurant
        {
            Id = guid,
            Name = "Test Restaurant",
            Description = "Test Description",
            Category = "Test Category",
            HasDelivery = true,
            ContactEmail = "test@test.com",
            ContactNumber = "123456789",
            Adress = new Adress
            {
                City = "Test City",
                Street = "Test Street",
                ZipCode = "12-345"
            },
            Dishes = []
        };

        // Act
        var result = p_Mapper.Map<RestaurantDto>(restaurant);

        // Assert
        result.Should().NotBeNull();
        result!.Id.Should().Be(restaurant.Id);
        result!.Name.Should().Be(restaurant.Name);
        result!.Description.Should().Be(restaurant.Description);
        result!.Category.Should().Be(restaurant.Category);
        result!.HasDelivery.Should().Be(restaurant.HasDelivery);
        result!.City.Should().Be(restaurant.Adress.City);
        result!.Street.Should().Be(restaurant.Adress.Street);
        result!.ZipCode.Should().Be(restaurant.Adress.ZipCode);
    }

    [Fact()]
    public void CreateMap_ForCreateRestaurantCommandToRestaurant_ShouldMapCorrectly()
    {
        // Arrange
        var createRestaurantCommand = new CreateRestaurantCommand
        {
            Name = "Test Restaurant",
            Description = "Test Description",
            Category = "Test Category",
            HasDelivery = true,
            ContactEmail = "test@test.com",
            ContactNumber = "123456789",
            City = "Test City",
            Street = "Test Street",
            ZipCode = "12-345"
        };

        // Act
        var result = p_Mapper.Map<Restaurant>(createRestaurantCommand);

        // Assert
        result.Should().NotBeNull();
        result!.Name.Should().Be(createRestaurantCommand.Name);
        result!.Description.Should().Be(createRestaurantCommand.Description);
        result!.Category.Should().Be(createRestaurantCommand.Category);
        result!.HasDelivery.Should().Be(createRestaurantCommand.HasDelivery);
        result!.ContactEmail.Should().Be(createRestaurantCommand.ContactEmail);
        result!.ContactNumber.Should().Be(createRestaurantCommand.ContactNumber);
        result!.Adress.Should().NotBeNull();
        result!.Adress!.City.Should().Be(createRestaurantCommand.City);
        result!.Adress!.Street.Should().Be(createRestaurantCommand.Street);
        result!.Adress!.ZipCode.Should().Be(createRestaurantCommand.ZipCode);
    }
}