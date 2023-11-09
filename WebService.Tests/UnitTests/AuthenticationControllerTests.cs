using Common.DataTransferObjects;
using DataLayer.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using WebService.Controllers;

namespace WebService.Tests.UnitTests;

public class AuthenticationControllerTests
{
    private readonly IAuthenticationService _authenticationService;
    private readonly AuthenticationController _controller;

    public AuthenticationControllerTests()
    {
        _authenticationService = Substitute.For<IAuthenticationService>();
        _controller = new AuthenticationController(_authenticationService);
    }
    
    [Fact]
    public async Task Register_ReturnsOkResult()
    {
        // Arrange
        var registerModel = new RegisterModel("User","User@user.com","User1234");
        _authenticationService.Register(registerModel).Returns(Task.CompletedTask);

        // Act
        var result = await _controller.Register(registerModel);

        // Assert
        var okResult = Assert.IsType<OkResult>(result);
    }

    [Fact]
    public async Task MakeAdmin_ReturnsOkResult()
    {
        // Arrange
        var username = "test-username";
        _authenticationService.MakeAdmin(username).Returns(Task.CompletedTask);

        // Act
        var result = await _controller.MakeAdmin(username);

        // Assert
        Assert.IsType<OkResult>(result);
    }

    [Fact]
    public async Task Login_ReturnsUnauthorizedOnError()
    {
        // Arrange
        var loginModel = new LoginModel("user","doncic");
        _authenticationService.Login(loginModel).Throws(new Exception("Test exception"));

        // Act
        var result = await _controller.Login(loginModel);

        // Assert
        var unauthorizedResult = Assert.IsType<UnauthorizedObjectResult>(result);
        Assert.Equal("Test exception", unauthorizedResult.Value);
    }

    [Fact]
    public async Task Register_ReturnsBadRequestOnError()
    {
        // Arrange
        var registerModel = new RegisterModel("Doncic77","doncic@to.com","user1234");
        _authenticationService.Register(registerModel).Throws(new Exception("Test exception"));

        // Act
        var result = await _controller.Register(registerModel);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Test exception", badRequestResult.Value);
    }

    [Fact]
    public async Task MakeAdmin_ReturnsBadRequestOnError()
    {
        // Arrange
        var username = "test-username";
        _authenticationService.MakeAdmin(username).Throws(new Exception("Test exception"));

        // Act
        var result = await _controller.MakeAdmin(username);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Test exception", badRequestResult.Value);
    }
}