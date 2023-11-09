using System.ComponentModel.DataAnnotations;
using Common.Domain;

namespace Common.DataTransferObjects;

public class UserDTO
{
    public string Id { get; set; }

    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public DateTime RegistrationDate { get; set; }
    public bool? IsAdmin { get; set; }

}

public class AlterUserDTO
{
    public string Id { get; set; }
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public DateTime RegistrationDate { get; set; }
}

/// <summary>
/// The RegisterModel class represents the data model for a registration operation.
/// </summary>
public record RegisterModel(
    // Username property is required for registration. It must be between 6 and 25 characters. If it's not provided or doesn't meet the length requirements, an error message will be displayed.
    [Required(ErrorMessage = "Username is required"), StringLength(25, ErrorMessage = "Username must be between 6 and 25 characters", MinimumLength = 6)] string Username, 
    
    // Email property is required for registration. It must be a valid email address. If it's not provided or is invalid, an error
    [Required(ErrorMessage = "Email is required"), EmailAddress(ErrorMessage = "Invalid email address")] string Email, 
    
    // Password property is required for registration. It must be between 8 and 32 characters. If it's not provided or doesn't meet the length requirements, an error message will be displayed.
    [Required(ErrorMessage = "Password is required"), StringLength(32, ErrorMessage = "Password must be between 8 and 32 characters", MinimumLength = 8)] string Password);

/// <summary>
/// The LoginModel class represents the data model for a login operation.
/// </summary>
public record LoginModel(
    // Username property is required for login. If it's not provided, an error message will be displayed.
    [Required(ErrorMessage = "Username is required")] string Username, 
    
    // Password property is required for login. If it's not provided, an error message will be displayed.
    [Required(ErrorMessage = "Password is required")] string Password);
    