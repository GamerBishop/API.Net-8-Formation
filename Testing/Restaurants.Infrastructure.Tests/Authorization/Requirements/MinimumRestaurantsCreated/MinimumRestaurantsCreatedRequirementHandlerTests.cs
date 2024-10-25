using FluentAssertions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Moq;
using Restaurants.Application.Users;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Authorization.Requirements.MinimumRestaurantsCreated;
using Xunit;

namespace Restaurants.Infrastructure.Tests.Authorization.Requirements.MinimumRestaurantsCreated;

public class MinimumRestaurantsCreatedRequirementHandlerTests
{

    private readonly Mock<ILogger<MinimumRestaurantsCreatedRequirementHandler>> p_LoggerMock;

    public MinimumRestaurantsCreatedRequirementHandlerTests()
    {
        p_LoggerMock = new Mock<ILogger<MinimumRestaurantsCreatedRequirementHandler>>();
    }

    [Theory]
    [InlineData(5, 5)]
    [InlineData(3, 4)]
    [InlineData(10, 15)]
    [InlineData(1, 1)]
    [InlineData(0, 1)]
    public async Task HandleRequirementAsync_UserHasCreatedMultipleRestaurants_ShouldSucceed(int limit, int testedValue)
    {
        // Arrange
        var currentUser = new CurrentUser("user-id", "user-email", [UserRoles.Owner], "German", DateOnly.MaxValue);
        var userContextMock = new Mock<IUserContext>();
        userContextMock.Setup(x => x.GetCurrentUser()).Returns(currentUser);

        var restaurantRepositoryMock = new Mock<IRestaurantRepository>();
        restaurantRepositoryMock.Setup(x => x.GetRestaurantsCountCreatedByUser("user-id")).ReturnsAsync(testedValue);

        var requirement = new MinimumRestaurantsCreatedRequirement(limit);
        var handler = new MinimumRestaurantsCreatedRequirementHandler(p_LoggerMock.Object, userContextMock.Object, restaurantRepositoryMock.Object);
        var context = new AuthorizationHandlerContext([requirement], null, null);

        // Act
        await handler.HandleAsync(context);

        // Assert
        context.HasSucceeded.Should().BeTrue();
    }

    [Theory]
    [InlineData(5, 4)]
    [InlineData(3, 2)]
    [InlineData(10, 9)]
    [InlineData(1, 0)]
    public async Task HandleRequirementAsync_UserHasNotCreatedMultipleRestaurants_ShouldFail(int limit, int testedValue)
    {
        // Arrange
        var currentUser = new CurrentUser("user-id", "user-email", [UserRoles.Owner], "German", DateOnly.MaxValue);
        var userContextMock = new Mock<IUserContext>();
        userContextMock.Setup(x => x.GetCurrentUser()).Returns(currentUser);

        var restaurantRepositoryMock = new Mock<IRestaurantRepository>();
        restaurantRepositoryMock.Setup(x => x.GetRestaurantsCountCreatedByUser("user-id")).ReturnsAsync(testedValue);

        var requirement = new MinimumRestaurantsCreatedRequirement(limit);
        var handler = new MinimumRestaurantsCreatedRequirementHandler(p_LoggerMock.Object, userContextMock.Object, restaurantRepositoryMock.Object);
        var context = new AuthorizationHandlerContext([requirement], null, null);

        // Act
        handler.HandleAsync(context);

        // Assert
        context.HasSucceeded.Should().BeFalse();
        context.HasFailed.Should().BeTrue();
    }

    [Fact()]
    public async Task HandleRequirementAsync_UserContextIsNull_ShouldFailAndThrowsNotFoundException()
    {
        // Arrange
        var userContextMock = new Mock<IUserContext>();
        userContextMock.Setup(x => x.GetCurrentUser()).Returns(() => null);

        var restaurantRepositoryMock = new Mock<IRestaurantRepository>();

        var requirement = new MinimumRestaurantsCreatedRequirement(5);
        var handler = new MinimumRestaurantsCreatedRequirementHandler(p_LoggerMock.Object, userContextMock.Object, restaurantRepositoryMock.Object);
        var context = new AuthorizationHandlerContext([requirement], null, null);

        // Act
        Func<Task> act = async () => await handler.HandleAsync(context);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
        context.HasSucceeded.Should().BeFalse();
    }
}