using Moq;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using dfdsMicroserviceProject.Controllers;
using dfdsMicroserviceProject.Services;
using dfdsMicroserviceProject.Models;

public class PassengerControllerTests
{
    private readonly Mock<IPassengerService> _mockPassengerService;
    private readonly PassengerController _controller;

    public PassengerControllerTests()
    {
        _mockPassengerService = new Mock<IPassengerService>();
        _controller = new PassengerController(_mockPassengerService.Object);
    }

    [Fact]
    public void GetExistingPassenger()
    {
        var passengerId = 1;
        var passenger = new Passenger { Id = passengerId, FirstName = "Tufan", LastName = "Usta", PassportNumber = "TR123456789", Nationality = "TR" };

        _mockPassengerService.Setup(service => service.GetPassengerById(passengerId)).Returns(passenger);

        var result = _controller.GetPassenger(passengerId);

        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedPassenger = Assert.IsType<Passenger>(okResult.Value);
        Assert.Equal(passengerId, returnedPassenger.Id);
    }

    [Fact]
    public void DeleteExistingPassenger()
    {
        int passengerId = 1;
        var passenger = new Passenger { Id = passengerId, FirstName = "Tufan", LastName = "Usta", PassportNumber = "TR123456789", Nationality = "TR" };

        _mockPassengerService.Setup(service => service.GetPassengerById(passengerId)).Returns(passenger);
        _mockPassengerService.Setup(service => service.DeletePassenger(passengerId)).Returns(true);

        var result = _controller.DeletePassenger(passengerId);

        Assert.IsType<NoContentResult>(result);

        _mockPassengerService.Verify(service => service.DeletePassenger(passengerId), Times.Once);
    }

    [Fact]
    public void DeleteNonExistingPassenger()
    {
        int passengerId = 999;
        _mockPassengerService.Setup(service => service.GetPassengerById(passengerId)).Returns((Passenger?)null);

        var result = _controller.DeletePassenger(passengerId);

        Assert.IsType<NotFoundResult>(result);
    }
}
