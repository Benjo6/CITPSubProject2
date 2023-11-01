using DataLayer.Identity;
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
    /// Asynchronously performs a login operation with the provided username and password.
    /// </summary>
    /// <param name="username">The username of the user to login.</param>
    /// <param name="password">The password of the user to login.</param>
    /// <returns>A Task that represents the asynchronous operation. The task result contains a string that represents the JWT token.</returns>
    /// <exception cref="Exception">Thrown when the username or password is invalid.</exception>
    public async Task<string> Login(string username, string password)
    {
        var user = await _repository.GetUserByUsername(username);

        if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.Password))
        {
            throw new Exception("Invalid username or password");
        }
        var claims = new List<Claim>
        {
            new Claim(Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames.Sub, user.Username)
        };

        if (user.IsAdmin)
        {
            claims.Add(new Claim(IdentityData.AdminUserClaimName, "true"));
        }

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]));
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
    /// Asynchronously performs a login operation with the provided username and password.
    /// </summary>
    /// <param name="username">The username of the user to login.</param>
    /// <param name="password">The password of the user to login.</param>
    /// <returns>A Task that represents the asynchronous operation. The task result contains a string that represents the JWT token.</returns>
    /// <exception cref="Exception">Thrown when the username or password is invalid.</exception>
    public async Task Register(string username, string email, string password)
    {
        var user = await _repository.GetUserByUsername(username);

        if (user != null)
        {
            throw new Exception("Username already taken.");
        }

        user = new Models.User
        {
            Username = username,
            Email = email,
            Password = BCrypt.Net.BCrypt.HashPassword(password),
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
            throw new Exception("User not found");
        }

        user.IsAdmin = true;
        await _repository.UpdateUser(user);
    }
}
