using System;
using System.Collections.Generic;

namespace SubProject2.Models;

public partial class Searchhistory
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public string SearchQuery { get; set; } = null!;

    public DateOnly SearchDate { get; set; }

    public virtual User? User { get; set; }
}
