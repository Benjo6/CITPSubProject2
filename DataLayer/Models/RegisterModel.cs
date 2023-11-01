using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models.DataTransferObjects;

/// <summary>
/// The RegisterModel class represents the data model for a registration operation.
/// </summary>
public class RegisterModel
{
    /// <summary>
    /// Username property is required for registration.
    /// It must be between 6 and 25 characters.
    /// If it's not provided or doesn't meet the length requirements,
    /// an error message will be displayed.
    /// </summary>
    [Required(ErrorMessage = "Username is required")]
    [StringLength(25, ErrorMessage = "Username must be between 6 and 25 characters", MinimumLength = 6)]
    public string Username { get; set; }

    /// <summary>
    ///  Email property is required for registration.
    ///  It must be a valid email address.
    ///  If it's not provided or is invalid, an error
    /// </summary>
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    public string Email { get; set; }

    /// <summary>
    /// Password property is required for registration. 
    /// It must be between 8 and 32 characters. 
    /// If it's not provided or doesn't meet the length requirements, 
    /// an error message will be displayed.
    /// </summary>
    [Required(ErrorMessage = "Password is required")]
    [StringLength(32, ErrorMessage = "Password must be between 8 and 32 characters", MinimumLength = 8)]
    public string Password { get; set; }
}

