using Xunit;
using Restaurants.Application.Restaurants.Commands.UpdateRestaurant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq; 
using AutoMapper;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Domain.Exceptions;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Constants;

namespace Restaurants.Application.Restaurants.Commands.UpdateRestaurant.Tests;

public class UpdateRestaurantCommandHandlerTests
{ 
    private readonly Mock<ILogger<UpdateRestaurantCommandHandler>> p_MockLogger;
    private readonly Mock<IMapper> p_MockMapper;
    private readonly Mock<IRestaurantAuthorizationService> p_MockRestaurantAuthorizationService;
    private readonly Mock<IRestaurantRepository> p_MockRestaurantRepository;

    private readonly Guid p_Guid = Guid.NewGuid();
    private readonly UpdateRestaurantCommand p_Command;

    public UpdateRestaurantCommandHandlerTests()
    {
        p_MockMapper = new Mock<IMapper>();
        p_MockMapper.Setup(m => m.Map<Restaurant>(It.IsAny<UpdateRestaurantCommand>())).Returns(new Restaurant());

        p_MockLogger = new Mock<ILogger<UpdateRestaurantCommandHandler>>();
        p_MockRestaurantAuthorizationService = new Mock<IRestaurantAuthorizationService>();
        p_Command = new UpdateRestaurantCommand(p_Guid) { Name = "Updated Name" };

        p_MockRestaurantRepository = new Mock<IRestaurantRepository>();
        p_MockRestaurantRepository.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(new Restaurant());
    }

    [Fact]
    public async Task Handle_ShouldUpdateRestaurant_WhenUserIsAuthorized()
    {
        // Arrange 
        p_MockRestaurantAuthorizationService.Setup(r => r.Authorize(It.IsAny<Restaurant>(), ResourceOperation.Update)).Returns(true);

        var handler = new UpdateRestaurantCommandHandler(p_MockLogger.Object, p_MockRestaurantRepository.Object, p_MockMapper.Object, p_MockRestaurantAuthorizationService.Object);

        // Act
        await handler.Handle(p_Command, CancellationToken.None);

        // Assert
        //Check if user authorized
         p_MockRestaurantRepository.Verify(r => r.GetByIdAsync(p_Guid), Times.Once);
         p_MockRestaurantAuthorizationService.Verify(r => r.Authorize(It.IsAny<Restaurant>(), ResourceOperation.Update), Times.Once);
         p_MockMapper.Verify(m => m.Map<UpdateRestaurantCommand, Restaurant>(p_Command, It.IsAny<Restaurant>()), Times.Once);
         p_MockRestaurantRepository.Verify(r => r.UpdateAsync(It.IsAny<Restaurant>()), Times.Once);
         
    }

    [Fact]
    public async Task Handle_ShouldThrowNotFoundException_WhenRestaurantDoesNotExist()
    {
        // Arrange
        p_MockRestaurantRepository.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Restaurant?)null);
        var handler = new UpdateRestaurantCommandHandler(p_MockLogger.Object, p_MockRestaurantRepository.Object, p_MockMapper.Object, p_MockRestaurantAuthorizationService.Object);

        // Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(p_Command, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_ShouldThrowForbidException_WhenUserIsNotAuthorized()
    {
        // Arrange
        // Simulate unauthorized user
        p_MockRestaurantAuthorizationService.Setup(r => r.Authorize(It.IsAny<Restaurant>(), ResourceOperation.Update)).Returns(false);

        var handler = new UpdateRestaurantCommandHandler(p_MockLogger.Object, p_MockRestaurantRepository.Object, p_MockMapper.Object, p_MockRestaurantAuthorizationService.Object);

        // Act & Assert
        await Assert.ThrowsAsync<ForbidException>(() => handler.Handle(p_Command, CancellationToken.None));
        // Assert that the UpdateAsync method was not called
        p_MockRestaurantRepository.Verify(r => r.UpdateAsync(It.IsAny<Restaurant>()), Times.Never);
    }
}
