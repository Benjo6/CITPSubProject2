using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Domain;

public class WiWeighted
{
    [StringLength(10), Column("tconst", TypeName = "char(10)")]
    public string? Tconst { get; set; }

    [Column("word", TypeName = "character varying")]
    public string? Word { get; set; }

    [StringLength(1), Column("field", TypeName = "char(1)")]
    public char? Field { get; set; }

    [Column("lexeme", TypeName = "character varying")]
    public string? Lexeme { get; set; }

    [Column("weight")]
    public double? Weight { get; set; }

    [ForeignKey("Tconst")]
    public Movie? TconstNavigation { get; set; }
}