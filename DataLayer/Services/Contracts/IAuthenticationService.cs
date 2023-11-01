namespace DataLayer.Services.Contracts
{
    /// <summary>
    /// The IAuthenticationService interface defines the operations related to user authentication services.
    /// </summary>
    public interface IAuthenticationService
    {
        /// <summary>
        /// Asynchronously performs a login operation with the provided username and password.
        /// </summary>
        /// <param name="username">The username of the user to login.</param>
        /// <param name="password">The password of the user to login.</param>
        /// <returns>A Task that represents the asynchronous operation. The task result contains a string that represents the login status.</returns>
        Task<string> Login(string username, string password);

        /// <summary>
        /// Asynchronously makes a user an admin with the provided username.
        /// </summary>
        /// <param name="username">The username of the user to make admin.</param>
        /// <returns>A Task that represents the asynchronous operation.</returns>
        Task MakeAdmin(string username);

        /// <summary>
        /// Asynchronously performs a registration operation with the provided username, email, and password.
        /// </summary>
        /// <param name="username">The username of the user to register.</param>
        /// <param name="email">The email of the user to register.</param>
        /// <param name="password">The password of the user to register.</param>
        /// <returns>A Task that represents the asynchronous operation.</returns>
        Task Register(string username, string email, string password);
    }
}
