using Common.Domain;
using DataLayer.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repositories
{
    /// <summary>
    /// The AuthenticationRepository class provides the implementation for the IAuthenticationRepository interface.
    /// It defines the operations related to user authentication.
    /// </summary>
    public class AuthenticationRepository : IAuthenticationRepository
    {
        // _context represents the database context used for data operations.
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
            // Add the user to the Users set in the context and save changes asynchronously.
            _ = _context.Users.Add(user);
            _ = await _context.SaveChangesAsync();
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
}
