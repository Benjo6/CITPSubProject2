using Common.DataTransferObjects;
using Common.Domain;
using Common.Identity;
using DataLayer.Repositories.Contracts;
using DataLayer.Services.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DataLayer.Services;

/// <summary>
/// The AuthenticationService class provides the implementation for the IAuthenticationService interface.
/// It defines the operations related to user authentication services.
/// </summary>
public class AuthenticationService : IAuthenticationService
{
    private readonly IConfiguration _configuration; // _configuration represents the application configuration.
    private readonly IAuthenticationRepository _repository;  // _repository represents the authentication repository.

    /// <summary>
    /// Initializes a new instance of the AuthenticationService class.
    /// </summary>
    /// <param name="configuration">The application configuration.</param>
    /// <param name="repository">The authentication repository.</param>
    public AuthenticationService(IConfiguration configuration, IAuthenticationRepository repository)
    {
        _configuration = configuration;
        _repository = repository;
    }

    /// <summary>
    /// Asynchronously performs a login operation with the provided LoginModel.
    /// </summary>
    /// <param name="model">The LoginModel containing the username and password of the user to login.</param>
    /// <returns>A Task that represents the asynchronous operation. The task result contains a string that represents the JWT token.</returns>
    /// <exception cref="Exception">Thrown when the username or password is invalid.</exception>
    public async Task<string> Login(LoginModel model)
    {
        var user = await _repository.GetUserByUsername(model.Username) ?? throw new ArgumentException("Invalid username or password");

        if (!BCrypt.Net.BCrypt.Verify(model.Password, user.Password))
        {
            throw new ArgumentException("Invalid username or password");
        }
        var claims = new List<Claim>
        {
            new(Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames.Sub, user.Username)
        };

        if (user.IsAdmin == true)
        {
            claims.Add(new Claim(IdentityData.AdminUserClaimName, "true"));
        }

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"] ?? throw new NullReferenceException("There is no key in your JwtSettings")));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["JwtSettings:Issuer"],
            audience: _configuration["JwtSettings:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    /// <summary>
    /// Asynchronously registers a new user.
    /// </summary>
    /// <param name="model">The RegisterModel containing the username, email, and password of the new user. The password will be hashed using BCrypt.</param>
    /// <exception cref="System.Exception">Thrown when the username is already taken.</exception>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async Task Register(RegisterModel model)
    {
        var user = await _repository.GetUserByUsername(model.Username);

        if (user != null)
        {
            throw new ArgumentException("Username already taken.");
        }

        user = new User
        {
            Username = model.Username,
            Email = model.Email,
            Password = BCrypt.Net.BCrypt.HashPassword(model.Password),
            IsAdmin = false
        };

        await _repository.CreateUser(user);
    }

    /// <summary>
    /// Asynchronously makes a user an admin with the provided username.
    /// </summary>
    /// <param name="username">The username of the user to make admin.</param>
    /// <returns>A Task that represents the asynchronous operation.</returns>
    /// <exception cref="Exception">Thrown when the user is not found.</exception>
    public async Task MakeAdmin(string username)
    {
        var user = await _repository.GetUserByUsername(username);
        if (user == null)
        {
            throw new ArgumentException("User not found");
        }

        user.IsAdmin = true;
        await _repository.UpdateUser(user);
    }
}
