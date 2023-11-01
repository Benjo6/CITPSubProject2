using DataLayer.Models;

namespace DataLayer.Repositories.Contracts;

/// <summary>
/// The IAuthenticationRepository interface defines the operations related to user authentication.
/// </summary>
public interface IAuthenticationRepository
{
    /// <summary>
    /// Asynchronously retrieves a User object that matches the provided username.
    /// </summary>
    /// <param name="username">The username of the user to retrieve.</param>
    /// <returns>A Task that represents the asynchronous operation. The task result contains the User object.</returns>
    Task<User> GetUserByUsername(string username);

    /// <summary>
    /// Asynchronously creates a new user in the database.
    /// </summary>
    /// <param name="user">The User object to create.</param>
    /// <returns>A Task that represents the asynchronous operation.</returns>
    Task CreateUser(User user);

    /// <summary>
    /// Asynchronously updates an existing user in the database.
    /// </summary>
    /// <param name="user">The User object to update.</param>
    /// <returns>A Task that represents the asynchronous operation.</returns>
    Task UpdateUser(User user);
}
