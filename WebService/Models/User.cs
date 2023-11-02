using System;
using System.Collections.Generic;

namespace SubProject2.Models;

public partial class User
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public DateOnly RegistrationDate { get; set; }

    public bool? Isadmin { get; set; }

    public virtual ICollection<Bookmarkmovie> Bookmarkmovies { get; set; } = new List<Bookmarkmovie>();

    public virtual ICollection<Bookmarkpersonality> Bookmarkpersonalities { get; set; } = new List<Bookmarkpersonality>();

    public virtual ICollection<Ratinghistory> Ratinghistories { get; set; } = new List<Ratinghistory>();

    public virtual ICollection<Searchhistory> Searchhistories { get; set; } = new List<Searchhistory>();
}
