using System;
using System.Collections.Generic;

namespace SubProject2.Models;

public partial class Ratinghistory
{
    public int UserId { get; set; }

    public string MovieId { get; set; } = null!;

    public int RatingValue { get; set; }

    public DateOnly RatingDate { get; set; }

    public virtual Movie Movie { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
