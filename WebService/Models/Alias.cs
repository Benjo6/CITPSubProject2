using System;
using System.Collections.Generic;

namespace SubProject2.Models;

public partial class Alias
{
    public int Id { get; set; }

    public string? MovieId { get; set; }

    public int? Ordering { get; set; }

    public string? Title { get; set; }

    public string? Region { get; set; }

    public string? Language { get; set; }

    public string? Types { get; set; }

    public string? Attributes { get; set; }

    public bool? IsOriginal { get; set; }

    public virtual ICollection<Bookmarkmovie> Bookmarkmovies { get; set; } = new List<Bookmarkmovie>();

    public virtual Movie? Movie { get; set; }
}
