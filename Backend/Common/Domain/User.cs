﻿using Riok.Mapperly.Abstractions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Domain;

/// <summary>
/// The User class represents the "user" table in the database.
/// </summary>
[Table("user")]
public class User
{
    /// <summary>
    /// Id property is the primary key for the User table. It's auto-generated by the database.
    /// </summary>
    [Key, Column("id"), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; } = null!;

    /// <summary>
    /// Username property represents the username of the user in the User table.
    /// </summary>
    [MaxLength(255)]
    [Column("username")]
    public string Username { get; set; } = null!;

    /// <summary>
    /// Email property represents the email of the user in the User table.
    /// </summary>
    [MaxLength(255)]
    [Column("email")]
    public string Email { get; set; } = null!;

    /// <summary>
    /// Password property represents the password of the user in the User table.
    /// </summary>
    [MaxLength(255)]
    [Column("password")]
    public string Password { get; set; } = null!;

    /// <summary>
    /// RegistrationDate property represents the date of registration of the user in the User table.
    /// </summary>
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    [Column("registration_date")]
    public DateTime? RegistrationDate { get; set; }

    /// <summary>
    /// IsAdmin property indicates whether the user is an admin or not in the User table.
    /// </summary>
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    [Column("isadmin")]
    public bool? IsAdmin { get; set; }

    public virtual ICollection<BookmarkMovie>? BookmarkMovies { get; set; }
    public virtual ICollection<BookmarkPersonality>? BookmarkPersonalities { get; set; }
    public virtual ICollection<RatingHistory>? RatingHistories { get; set; }
    public virtual ICollection<SearchHistory>? SearchHistories { get; set; }
}
