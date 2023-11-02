using System;
using System.Collections.Generic;

namespace SubProject2.Models;

public partial class Episode
{
    public string Id { get; set; } = null!;

    public string? SeriesId { get; set; }

    public int? Season { get; set; }

    public int? Episode1 { get; set; }

    public virtual Movie? Series { get; set; }
}
