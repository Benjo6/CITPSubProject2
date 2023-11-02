using System;
using System.Collections.Generic;

namespace SubProject2.Models;

public partial class Role
{
    public string? MovieId { get; set; }

    public string? PersonId { get; set; }

    public int? Ordering { get; set; }

    public string? Category { get; set; }

    public string? Job { get; set; }

    public string? Characters { get; set; }

    public virtual Movie? Movie { get; set; }

    public virtual Person? Person { get; set; }
}
