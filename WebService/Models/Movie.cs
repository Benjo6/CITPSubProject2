using System;
using System.Collections.Generic;

namespace SubProject2.Models;

public partial class Movie
{
    public string Id { get; set; } = null!;

    public string? Type { get; set; }

    public string? Title { get; set; }

    public string? OriginalTitle { get; set; }

    public bool? IsAdult { get; set; }

    public string? StartYear { get; set; }

    public string? EndYear { get; set; }

    public int? Runtime { get; set; }

    public string? Genres { get; set; }

    public decimal? Rating { get; set; }

    public int? Votes { get; set; }

    public virtual ICollection<Alias> Aliases { get; set; } = new List<Alias>();

    public virtual ICollection<Episode> Episodes { get; set; } = new List<Episode>();

    public virtual ICollection<Ratinghistory> Ratinghistories { get; set; } = new List<Ratinghistory>();
}
