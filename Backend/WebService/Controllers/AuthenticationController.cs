using Common.DataTransferObjects;
using Common.Identity;
using DataLayer.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebService.Controllers;

/// <summary>
/// The AuthenticationController class handles the HTTP requests related to user authentication.
/// </summary>
[ApiController]
[Route("[controller]")]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthenticationService
        _authenticationService; // _authenticationService represents the authentication service.

    /// <summary>
    /// Initializes a new instance of the AuthenticationController class.
    /// </summary>
    /// <param name="authenticationService">The authentication service.</param>
    public AuthenticationController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    /// <summary>
    /// Asynchronously performs a login operation with the provided login model.
    /// </summary>
    /// <param name="model">The login model.</param>
    /// <returns>A Task that represents the asynchronous operation. The task result contains an IActionResult that represents the result of the login operation.</returns>
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel model)
    {
        try
        {
            var token = await _authenticationService.Login(model);
            return Ok(new { token });
        }
        catch (Exception ex)
        {
            return Unauthorized(ex.Message);
        }
    }

    /// <summary>
    /// Asynchronously performs a registration operation with the provided register model.
    /// </summary>
    /// <param name="model">The register model.</param>
    /// <returns>A Task that represents the asynchronous operation. The task result contains an IActionResult that represents the result of the registration operation.</returns>
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterModel model)
    {
        try
        {
            await _authenticationService.Register(model);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Asynchronously makes a user an admin with the provided username.
    /// </summary>
    /// <param name="username">The username of the user to make admin.</param>
    /// <returns>A Task that represents the asynchronous operation. 
    /// The task result contains an IActionResult that represents the result of the make admin operation.</returns>
    [HttpPut("makeAdmin")]
    [Authorize(Policy = IdentityData.AdminUserPolicyName)]
    public async Task<IActionResult> MakeAdmin([FromBody] string username)
    {
        try
        {
            await _authenticationService.MakeAdmin(username);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}