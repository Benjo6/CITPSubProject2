using Common;
using Common.DataTransferObjects;
using Common.Domain;
using DataLayer.Generics;
using DataLayer.Services;
using NSubstitute;

namespace DataLayer.Tests.Services;

public class UserServiceTests
{
    private readonly IGenericRepository<User> _repository;
    private readonly UserService _service;
    private Filter _filter;

    public UserServiceTests()
    {
        _repository = Substitute.For<IGenericRepository<User>>();
        _service = new UserService(_repository);
        _filter = new Filter();
    }

    [Fact]
    public async Task GetAllUser_ReturnsAllUsers()
    {
        // Arrange
        var users = new List<User> { new(), new() };
        _repository.GetAll(_filter).Returns(users);

        // Act
        var result = await _service.GetAllUser(_filter);

        // Assert
        Assert.Equal(users.Count, result.Count);
    }

    [Fact]
    public async Task GetOneUser_ReturnsUserById()
    {
        // Arrange
        var userId = "1";
        var user = new User();
        _repository.GetById(userId).Returns(user);

        // Act
        var result = await _service.GetOneUser(userId);

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public async Task UpdateUser_ReturnsTrueOnUpdate()
    {
        // Arrange
        var userId = "1";
        var alterUser = new AlterUserDTO();
        var user = new User();
        _repository.Update(Arg.Any<User>()).Returns(true);
        _repository.GetById(userId).Returns(user);

        // Act
        var result = await _service.UpdateUser(userId, alterUser);

        // Assert
        Assert.True(result);
    }
    
    [Fact]
    public async Task DeleteUser_ReturnsTrueOnDeleteSuccess()
    {
        // Arrange
        var userId = "1";
        _repository.GetById(Arg.Any<string>()).Returns(new User());
        _repository.Delete(Arg.Any<User>()).Returns(true);

        // Act
        var result = await _service.DeleteUser(userId);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task DeleteUser_ReturnsFalseOnDeleteFailure()
    {
        // Arrange
        var userId = "1";
        _repository.GetById(Arg.Any<string>()).Returns(new User());
        _repository.Delete(Arg.Any<User>()).Returns(false);

        // Act
        var result = await _service.DeleteUser(userId);

        // Assert
        Assert.False(result);
    }
}