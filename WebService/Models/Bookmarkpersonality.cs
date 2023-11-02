using System;
using System.Collections.Generic;

namespace SubProject2.Models;

public partial class Bookmarkpersonality
{
    public int UserId { get; set; }

    public string PersonId { get; set; } = null!;

    public DateOnly BookmarkDate { get; set; }

    public virtual Person Person { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
