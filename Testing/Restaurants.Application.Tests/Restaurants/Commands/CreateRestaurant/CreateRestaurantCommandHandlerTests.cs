using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Restaurants.Application.Users;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant.Tests;

public class CreateRestaurantCommandHandlerTests
{
    [Fact()]
    public async Task Handle_ForValidCommand_ReturnCreatedRestaurantId()
    {
        // Arrange
        var loggerMock = new Mock<ILogger<CreateRestaurantCommandHandler>>();
        var mapperMock = new Mock<IMapper>();

        var restaurant = new Restaurant();

        mapperMock
            .Setup(x => x.Map<Restaurant>(It.IsAny<CreateRestaurantCommand>()))
            .Returns(restaurant);

        var _guid = Guid.NewGuid();

        var restaurantRepositoryMock = new Mock<IRestaurantRepository>();
        restaurantRepositoryMock
            .Setup(x => x.CreateAsync(It.IsAny<Restaurant>()))
            .ReturnsAsync(_guid);

        var userContextMock = new Mock<IUserContext>();
        var CurrentUser = new CurrentUser("owner-id", "test@test.com", [], null, null);
        userContextMock.Setup(x => x.GetCurrentUser()).Returns(CurrentUser);

        var commandHandler = new CreateRestaurantCommandHandler(loggerMock.Object, mapperMock.Object, restaurantRepositoryMock.Object, userContextMock.Object);

        // Act
        var result = await commandHandler.Handle(new CreateRestaurantCommand(), CancellationToken.None);

        // Assert
        result.Should().Be(_guid);
        restaurant.OwnerId.Should().Be(CurrentUser.Id);
        restaurantRepositoryMock.Verify(x => x.CreateAsync(restaurant), Times.Once);
    } 

    [Fact()]
    public async Task Handle_WhenRepositoryThrowsException_ThrowsException()
    {
        // Arrange
        var loggerMock = new Mock<ILogger<CreateRestaurantCommandHandler>>();
        var mapperMock = new Mock<IMapper>();

        var restaurant = new Restaurant();
        mapperMock
            .Setup(x => x.Map<Restaurant>(It.IsAny<CreateRestaurantCommand>()))
            .Returns(restaurant);

        var restaurantRepositoryMock = new Mock<IRestaurantRepository>();
        restaurantRepositoryMock
            .Setup(x => x.CreateAsync(It.IsAny<Restaurant>()))
            .ThrowsAsync(new Exception("Repository error"));

        var userContextMock = new Mock<IUserContext>();
        var CurrentUser = new CurrentUser("owner-id", "test@test.com", [], null, null);
        userContextMock.Setup(x => x.GetCurrentUser()).Returns(CurrentUser);

        var commandHandler = new CreateRestaurantCommandHandler(loggerMock.Object, mapperMock.Object, restaurantRepositoryMock.Object, userContextMock.Object);

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => commandHandler.Handle(new CreateRestaurantCommand(), CancellationToken.None));
    }

    [Fact()]
    public async Task Handle_WhenCurrentUserNotFound_ThrowsNotFoundException()
    {
        // Arrange
        var loggerMock = new Mock<ILogger<CreateRestaurantCommandHandler>>();
        var mapperMock = new Mock<IMapper>();
        var restaurantRepositoryMock = new Mock<IRestaurantRepository>();
        var userContextMock = new Mock<IUserContext>();

        userContextMock.Setup(x => x.GetCurrentUser()).Returns((CurrentUser?)null);

        var commandHandler = new CreateRestaurantCommandHandler(loggerMock.Object, mapperMock.Object, restaurantRepositoryMock.Object, userContextMock.Object);

        // Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(() => commandHandler.Handle(new CreateRestaurantCommand(), CancellationToken.None));
    }
}
