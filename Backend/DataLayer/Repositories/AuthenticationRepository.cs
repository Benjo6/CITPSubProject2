using Common.Domain;
using DataLayer.Infrastructure;
using DataLayer.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace DataLayer.Repositories;

/// <summary>
/// The AuthenticationRepository class provides the implementation for the IAuthenticationRepository interface.
/// It defines the operations related to user authentication.
/// </summary>
public class AuthenticationRepository : IAuthenticationRepository
{
    /// <summary>
    /// _context represents the database context used for data operations.
    /// </summary>
    private AppDbContext _context;

    /// <summary>
    /// Initializes a new instance of the AuthenticationRepository class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public AuthenticationRepository(AppDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Asynchronously creates a new user in the database.
    /// </summary>
    /// <param name="user">The User object to create.</param>
    /// <returns>A Task that represents the asynchronous operation.</returns>
    public async Task CreateUser(User user)
    {
        // Create parameters for the add_user function.
        var usernameParam = new NpgsqlParameter("@username", user.Username);
        var passwordParam = new NpgsqlParameter("@password", user.Password);
        var emailParam = new NpgsqlParameter("@email", user.Email);
        var isAdminParam = new NpgsqlParameter("@isAdmin", user.IsAdmin);

        // Call the add_user function.
        await _context.Database.ExecuteSqlRawAsync("SELECT add_user(@username, @password, @email,@isAdmin)", usernameParam, passwordParam, emailParam, isAdminParam);
    }


    /// <summary>
    /// Asynchronously retrieves a User object that matches the provided username.
    /// </summary>
    /// <param name="username">The username of the user to retrieve.</param>
    /// <returns>A Task that represents the asynchronous operation. The task result contains the User object.</returns>
    public async Task<User> GetUserByUsername(string username)
    {
        // Find and return the first user that matches the provided username asynchronously.
        return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
    }

    /// <summary>
    /// Asynchronously updates an existing user in the database.
    /// </summary>
    /// <param name="user">The User object to update.</param>
    /// <returns>A Task that represents the asynchronous operation.</returns>
    public async Task UpdateUser(User user)
    {
        // Update the user in the Users set in the context and save changes asynchronously.
        _ = _context.Users.Update(user);
        _ = await _context.SaveChangesAsync();
    }
}