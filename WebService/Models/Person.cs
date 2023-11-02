using System;
using System.Collections.Generic;

namespace SubProject2.Models;

public partial class Person
{
    public string Id { get; set; } = null!;

    public string? Name { get; set; }

    public string? BirthYear { get; set; }

    public string? DeathYear { get; set; }

    public string? Professions { get; set; }

    public string? KnownFor { get; set; }

    public virtual ICollection<Bookmarkpersonality> Bookmarkpersonalities { get; set; } = new List<Bookmarkpersonality>();
}
