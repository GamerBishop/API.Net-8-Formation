using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Application.Users;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Tests.Restaurants.Commands.CreateRestaurant;

public class CreateRestaurantCommandHandlerTests
{

    private readonly Mock<ILogger<CreateRestaurantCommandHandler>> p_LoggerMock;
    private readonly Mock<IMapper> p_MapperMock;

    public CreateRestaurantCommandHandlerTests()
    {
        p_LoggerMock = new Mock<ILogger<CreateRestaurantCommandHandler>>();
        p_MapperMock = new Mock<IMapper>();
    }

    [Fact()]
    public async Task Handle_ForValidCommand_ReturnCreatedRestaurantId()
    {
        // Arrange 
        var restaurant = new Restaurant();

        p_MapperMock
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

        var commandHandler = new CreateRestaurantCommandHandler(p_LoggerMock.Object, p_MapperMock.Object, restaurantRepositoryMock.Object, userContextMock.Object);

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
        var restaurant = new Restaurant();
        p_MapperMock
            .Setup(x => x.Map<Restaurant>(It.IsAny<CreateRestaurantCommand>()))
            .Returns(restaurant);

        var restaurantRepositoryMock = new Mock<IRestaurantRepository>();
        restaurantRepositoryMock
            .Setup(x => x.CreateAsync(It.IsAny<Restaurant>()))
            .ThrowsAsync(new Exception("Repository error"));

        var userContextMock = new Mock<IUserContext>();
        var CurrentUser = new CurrentUser("owner-id", "test@test.com", [], null, null);
        userContextMock.Setup(x => x.GetCurrentUser()).Returns(CurrentUser);

        var commandHandler = new CreateRestaurantCommandHandler(p_LoggerMock.Object, p_MapperMock.Object, restaurantRepositoryMock.Object, userContextMock.Object);

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => commandHandler.Handle(new CreateRestaurantCommand(), CancellationToken.None));
    }

    [Fact()]
    public async Task Handle_WhenCurrentUserNotFound_ThrowsNotFoundException()
    {
        // Arrange
        var restaurantRepositoryMock = new Mock<IRestaurantRepository>();
        var userContextMock = new Mock<IUserContext>();

        userContextMock.Setup(x => x.GetCurrentUser()).Returns((CurrentUser?)null);

        var commandHandler = new CreateRestaurantCommandHandler(p_LoggerMock.Object, p_MapperMock.Object, restaurantRepositoryMock.Object, userContextMock.Object);

        // Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(() => commandHandler.Handle(new CreateRestaurantCommand(), CancellationToken.None));
    }
}
