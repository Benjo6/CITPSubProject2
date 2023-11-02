using System;
using System.Collections.Generic;

namespace SubProject2.Models;

public partial class WiWeighted
{
    public string? Tconst { get; set; }

    public string? Word { get; set; }

    public char? Field { get; set; }

    public string? Lexeme { get; set; }

    public double? Weight { get; set; }

    public virtual Movie? TconstNavigation { get; set; }
}
