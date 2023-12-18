using Common;
using Common.DataTransferObjects;
using DataLayer.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Newtonsoft.Json;
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

        var urlHelper = Substitute.For<IUrlHelper>();
        urlHelper.Action(Arg.Any<UrlActionContext>()).Returns("callbackUrl");
        _controller.Url = urlHelper;
    }

    [Fact]
    public async Task GetUsers_ReturnsOkResult()
    {
        // Arrange
        var expectedUsers = new List<UserDTO>();
        _service.GetAllUser(Arg.Any<Filter>()).Returns(expectedUsers);

        // Act
        var result = await _controller.GetUsers();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var json = JsonConvert.SerializeObject(okResult.Value);
        var data = JsonConvert.DeserializeObject<UsersResult>(json);
        List<UserDTO> returnedUserDTOs = data.Users.Select(a => a.User).ToList();
        Assert.Equal(expectedUsers, returnedUserDTOs);
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
        var json = JsonConvert.SerializeObject(okResult.Value);
        var data = JsonConvert.DeserializeObject<UserWithUri>(json);
        var model = data.User;
        Assert.Equal(expectedUser.Id, model.Id);
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
        var json = JsonConvert.SerializeObject(okResult.Value);
        var data = JsonConvert.DeserializeObject<UsersBooleanResult>(json);
        var model = data.Result;
        Assert.False(model);
    }

    [Fact]
    public async Task DeleteUser_ReturnsOkResult()
    {
        // Arrange
        var userId = "1";
        var deletionResult = true;
        _service.DeleteUser(userId).Returns(deletionResult);

        // Act
        var result = await _controller.DeleteUser(userId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var json = JsonConvert.SerializeObject(okResult.Value);
        var data = JsonConvert.DeserializeObject<UsersBooleanResult>(json);
        Assert.True(data.Result);
    }

    [Fact]
    public async Task GetUsers_ReturnsBadRequestOnError()
    {
        // Arrange
        _service.GetAllUser(Arg.Any<Filter>()).Throws(new Exception("Test exception"));

        // Act
        var result = await _controller.GetUsers();

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Contains("Test exception", badRequestResult.Value.ToString());
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
        Assert.Contains("Test exception", badRequestResult.Value.ToString());
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
        Assert.Contains("Test exception", badRequestResult.Value.ToString());
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
        Assert.Contains("Test exception", badRequestResult.Value.ToString());
    }
}

public class UserWithUri
{
    public UserDTO User { get; set; }
    public string Uri { get; set; }
}

public class UsersResult
{
    public IEnumerable<UserWithUri> Users { get; set; }
    public string Uri { get; set; }
}


public class UsersBooleanResult
{
    public bool Result { get; set; }
    public string Uri { get; set; }
}