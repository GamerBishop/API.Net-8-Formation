using Moq;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Restaurants.Domain.Constants;
using FluentAssertions;

namespace Restaurants.Application.Users.Tests
{
    public class UserContextTests
    {
        [Fact()]
        public void GetCurrentUser_WithAuthenticatedUser_ShouldReturnCurrentUser()
        {
            // Arrange
            var htttpContextAccessorMock = new Mock<IHttpContextAccessor>();

            var dateOfBirth = new DateOnly(1990, 1, 1);
            var claims = new List<Claim>
            {
                new (ClaimTypes.NameIdentifier, "1"),
                new (ClaimTypes.Email, "test@test.com"),
                new (ClaimTypes.Role, UserRoles.Admin),
                new (ClaimTypes.Role, UserRoles.User),
                new ("Nationality", "German"),
                new ("BirthDate", dateOfBirth.ToString("yyyy-MM-dd"))
            };

            var user = new ClaimsPrincipal(new ClaimsIdentity(claims, "TestAuthType"));

            htttpContextAccessorMock.Setup(x => x.HttpContext ).Returns(new DefaultHttpContext()
            {
                User = user
            });

            var userContext = new UserContext(htttpContextAccessorMock.Object);

            //Act
            var currentUser = userContext.GetCurrentUser();

            //Assert
            currentUser.Should().NotBeNull();
            currentUser!.Id.Should().Be("1");
            currentUser!.Email.Should().Be("test@test.com");
            currentUser!.Nationality.Should().Be("German");
            currentUser!.BirthDate.Should().Be(dateOfBirth);
            currentUser!.IsEnroledIn(UserRoles.Admin).Should().BeTrue();
            currentUser!.IsEnroledIn(UserRoles.User).Should().BeTrue();

        }

        [Fact]
        public void GetCurrentUser_WithUserContextNitPresent_ShouldThrowInvalidOperationException()
        {
            // Arrange
            var htttpContextAccessorMock = new Mock<IHttpContextAccessor>();

            htttpContextAccessorMock.Setup(x => x.HttpContext).Returns((HttpContext?)null);

            var userContext = new UserContext(htttpContextAccessorMock.Object);

            //Act
            Action act = () => userContext.GetCurrentUser();

            //Assert
            act.Should().Throw<InvalidOperationException>().WithMessage("User context is not present.");
        }
    }
}