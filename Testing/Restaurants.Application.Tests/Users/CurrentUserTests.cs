using FluentAssertions;
using Restaurants.Domain.Constants;

namespace Restaurants.Application.Users.Tests;

public class CurrentUserTests
{
    [Theory()]
    [InlineData(UserRoles.Admin)]
    [InlineData(UserRoles.User)]
    public void IsEnroledIn_WithMatchingRole_ShouldReturnTrue(string roleName)
    {
        // Arrange
        var roles = new List<string> { UserRoles.Admin, UserRoles.User };
        var currentUser = new CurrentUser("1", "test@text.com", roles, "US", new DateOnly(1990, 1, 1));

        // Act
        var result = currentUser.IsEnroledIn(roleName);

        //Assert
        result.Should().BeTrue();
    }

    [Fact()]
    public void IsEnroledIn_WithNoMatchingRole_ShouldReturnFalse()
    {
        // Arrange
        var roles = new List<string> { UserRoles.Admin, UserRoles.User };
        var currentUser = new CurrentUser("1", "test@text.com", roles, "US", new DateOnly(1990, 1, 1));

        //Act
        var result = currentUser.IsEnroledIn(UserRoles.Owner);

        //Assert
        result.Should().BeFalse();
    }
}