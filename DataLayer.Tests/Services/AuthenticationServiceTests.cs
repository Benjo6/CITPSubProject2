using Common.DataTransferObjects;
using Common.Domain;
using DataLayer.Repositories.Contracts;
using DataLayer.Services;
using Microsoft.Extensions.Configuration;
using NSubstitute;

namespace DataLayer.Tests.Services;

public class AuthenticationServiceTests
{
    private readonly IConfiguration _configuration;
    private readonly IAuthenticationRepository _repository;
    private readonly AuthenticationService _service;

    public AuthenticationServiceTests()
    {
        _configuration = Substitute.For<IConfiguration>();
        _repository = Substitute.For<IAuthenticationRepository>();
        _service = new AuthenticationService(_configuration, _repository);
    }

    [Fact]
    public async Task Login_ValidUser_ReturnsToken()
    {
        // Arrange
        var username = "testuser";
        var password = "testpassword";
        var user = new User
        {
            Username = username,
            Password = BCrypt.Net.BCrypt.HashPassword(password)
        };

        _repository.GetUserByUsername(username).Returns(user);
        _configuration["JwtSettings:Key"].Returns("your-secret-key3434854364365364654632");

        // Act
        var loginModel = new LoginModel(username,password);
        var result = await _service.Login(loginModel);

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public async Task Login_InvalidUser_ThrowsException()
    {
        // Arrange
        var username = "testuser";
        var password = "testpassword";
        var user = new User
        {
            Username = username,
            Password = BCrypt.Net.BCrypt.HashPassword("wrongpassword")
        };

        _repository.GetUserByUsername(username).Returns(user);

        // Act
        var loginModel = new LoginModel(username, password);

        // Assert
        await Assert.ThrowsAsync<ArgumentException>(() => _service.Login(loginModel));
    }

    [Fact]
    public async Task Register_NewUser_ReturnsTask()
    {
        // Arrange
        var username = "newuser";
        var email = "newuser@example.com";
        var password = "newpassword";
        _repository.GetUserByUsername(username).Returns((User)null);

        // Act
        var registerModel = new RegisterModel(username, email, password);

        // Assert
        await _service.Register(registerModel);
    }

    [Fact]
    public async Task Register_ExistingUser_ThrowsException()
    {
        // Arrange
        var username = "existinguser";
        var email = "existinguser@example.com";
        var password = "existingpassword";
        var existingUser = new User
        {
            Username = username,
            Email = email,
            Password = BCrypt.Net.BCrypt.HashPassword(password)
        };

        _repository.GetUserByUsername(username).Returns(existingUser);

        // Act
        var registerModel = new RegisterModel(username, email, password);

        // Assert
        await Assert.ThrowsAsync<ArgumentException>(() => _service.Register(registerModel));
    }

    [Fact]
    public async Task MakeAdmin_ValidUser_ReturnsTask()
    {
        // Arrange
        var username = "testuser";
        var user = new User
        {
            Username = username
        };

        _repository.GetUserByUsername(username).Returns(user);

        // Act
        await _service.MakeAdmin(username);
    }

    [Fact]
    public async Task MakeAdmin_InvalidUser_ThrowsException()
    {
        // Arrange
        var username = "nonexistentuser";

        _repository.GetUserByUsername(username).Returns((User)null);

        // Act and Assert
        await Assert.ThrowsAsync<ArgumentException>(() => _service.MakeAdmin(username));
    }
}