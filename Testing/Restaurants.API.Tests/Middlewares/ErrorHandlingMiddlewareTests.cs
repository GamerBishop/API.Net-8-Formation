using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using Restaurants.API.Middlewares;
using Restaurants.Domain.Exceptions;
using System.Net;
using Xunit;

namespace Restaurants.API.Tests.Middlewares;

public class ErrorHandlingMiddlewareTests
{
    private readonly Mock<ILogger<ErrorHandlingMiddleware>> p_LoggerMock;
    private readonly ErrorHandlingMiddleware p_Middleware;

    public ErrorHandlingMiddlewareTests()
    {
        p_LoggerMock = new Mock<ILogger<ErrorHandlingMiddleware>>();
        p_Middleware = new ErrorHandlingMiddleware(p_LoggerMock.Object);
    }

    [Fact()]
    public async Task InvokeAsync_WhenNoExceptionIsThrown_ShouldCallNextDelegate()
    {
        // Arrange
        var context = new DefaultHttpContext();
        var nextDelegateMock = new Mock<RequestDelegate>();

        // Act
        await p_Middleware.InvokeAsync(context, nextDelegateMock.Object);

        // Assert
        nextDelegateMock.Verify(next => next(context), Times.Once);
    }

    [Fact()]
    public async Task InvokeAsync_WhenNotFoundExceptionIsThrown_ShouldReturnNotFoundStatusCode()
    {
        // Arrange
        var context = new DefaultHttpContext();
        var nextDelegateMock = new Mock<RequestDelegate>();
        nextDelegateMock.Setup(next => next(context)).ThrowsAsync(new NotFoundException("Not found","NotFoundTest"));

        // Act
        await p_Middleware.InvokeAsync(context, nextDelegateMock.Object);

        // Assert
        context.Response.StatusCode.Should().Be((int)HttpStatusCode.NotFound);
    }

    [Fact()]
    public async Task InvokeAsync_WhenNotFoundExceptionIsThrown_ShouldLogError()
    {
        // Arrange
        var context = new DefaultHttpContext();
        var nextDelegateMock = new Mock<RequestDelegate>();
        var exception = new NotFoundException("Not found", "NotFoundTest");
        nextDelegateMock.Setup(next => next(context)).ThrowsAsync(exception);

        // Act
        await p_Middleware.InvokeAsync(context, nextDelegateMock.Object);

        // Assert
        p_LoggerMock.Verify(logger => logger.LogError(exception, "An unhandled exception has occurred"), Times.Once);
    }
}
