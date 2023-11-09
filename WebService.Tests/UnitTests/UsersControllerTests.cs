using Common.DataTransferObjects;
using DataLayer.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using WebService.Controllers;

namespace WebService.Tests.UnitTests;

public class UsersControllerTests
{
    private readonly IUserService _service;
    private readonly UsersController _controller;

    public UsersControllerTests()
    {
        _service = Substitute.For<IUserService>();
        _controller = new UsersController(_service);
    }

    [Fact]
    public async Task GetUsers_ReturnsOkResult()
    {
        // Arrange
        var expectedUsers = new List<UserDTO>();
        _service.GetAllUser().Returns(expectedUsers);

        // Act
        var result = await _controller.GetUsers();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var model = Assert.IsAssignableFrom<IEnumerable<UserDTO>>(okResult.Value);
        Assert.Equal(expectedUsers, model);
    }

    [Fact]
    public async Task GetUser_ReturnsOkResult()
    {
        // Arrange
        var userId = "1";
        var expectedUser = new UserDTO();
        _service.GetOneUser(userId).Returns(expectedUser);

        // Act
        var result = await _controller.GetUser(userId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var model = Assert.IsType<UserDTO>(okResult.Value);
        Assert.Equal(expectedUser, model);
    }

    [Fact]
    public async Task PutUser_ReturnsOkResult()
    {
        // Arrange
        var userId = "1";
        var user = new AlterUserDTO();
        _service.UpdateUser(userId, user).Returns(true);

        // Act
        var result = await _controller.PutUser(userId, user);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var statement = Assert.IsType<bool>(okResult.Value);
        Assert.True(statement);
    }

    [Fact]
    public async Task DeleteUser_ReturnsOkResult()
    {
        // Arrange
        var userId = "1";
        var deletionResult = true; // Adjust as needed
        _service.DeleteUser(userId).Returns(deletionResult);

        // Act
        var result = await _controller.DeleteUser(userId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(deletionResult, okResult.Value);
    }

    [Fact]
    public async Task GetUsers_ReturnsBadRequestOnError()
    {
        // Arrange
        _service.GetAllUser().Throws(new Exception("Test exception"));

        // Act
        var result = await _controller.GetUsers();

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Test exception", badRequestResult.Value);
    }

    [Fact]
    public async Task GetUser_ReturnsBadRequestOnError()
    {
        // Arrange
        var userId = "1";
        _service.GetOneUser(userId).Throws(new Exception("Test exception"));

        // Act
        var result = await _controller.GetUser(userId);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Test exception", badRequestResult.Value);
    }

    [Fact]
    public async Task PutUser_ReturnsBadRequestOnError()
    {
        // Arrange
        var userId = "1";
        var user = new AlterUserDTO();
        _service.UpdateUser(userId, user).Throws(new Exception("Test exception"));

        // Act
        var result = await _controller.PutUser(userId, user);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Test exception", badRequestResult.Value);
    }

    [Fact]
    public async Task DeleteUser_ReturnsBadRequestOnError()
    {
        // Arrange
        var userId = "1";
        _service.DeleteUser(userId).Throws(new Exception("Test exception"));

        // Act
        var result = await _controller.DeleteUser(userId);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Test exception", badRequestResult.Value);
    }
}