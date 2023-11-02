using System;
using System.Collections.Generic;

namespace SubProject2.Models;

public partial class Bookmarkmovie
{
    public int UserId { get; set; }

    public int AliasId { get; set; }

    public DateOnly BookmarkDate { get; set; }

    public string? Note { get; set; }

    public virtual Alias Alias { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
