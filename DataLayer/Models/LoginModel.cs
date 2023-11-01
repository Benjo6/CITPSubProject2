using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models.DataTransferObjects;

/// <summary>
/// The LoginModel class represents the data model for a login operation.
/// </summary>
public class LoginModel
{
    /// <summary>
    /// Username property is required for login. If it's not provided, an error message will be displayed.
    /// </summary>
    [Required(ErrorMessage = "Username is required")]
    public string Username { get; set; }

    /// <summary>
    /// Password property is required for login. If it's not provided, an error message will be displayed.
    /// </summary>
    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; }
}
