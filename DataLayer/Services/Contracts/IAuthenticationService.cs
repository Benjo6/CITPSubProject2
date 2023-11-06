using Common.DataTransferObjects;

namespace DataLayer.Services.Contracts
{
    /// <summary>
    /// The IAuthenticationService interface defines the operations related to user authentication services.
    /// </summary>
    public interface IAuthenticationService
    {
        /// <summary>
        /// Asynchronously performs a login operation with the provided LoginModel.
        /// </summary>
        /// <param name="model">The LoginModel containing the username and password of the user to login.</param>
        /// <returns>A Task that represents the asynchronous operation. The task result contains a string that represents the login status.</returns>
        Task<string> Login(LoginModel model);

        /// <summary>
        /// Asynchronously makes a user an admin with the provided username.
        /// </summary>
        /// <param name="username">The username of the user to make admin.</param>
        /// <returns>A Task that represents the asynchronous operation.</returns>
        Task MakeAdmin(string username);

        /// <summary>
        /// Asynchronously performs a registration operation with the provided RegisterModel.
        /// </summary>
        /// <param name="model">The RegisterModel containing the username, email, and password of the user to register.</param>
        /// <returns>A Task that represents the asynchronous operation.</returns>
        Task Register(RegisterModel model);
    }
}
