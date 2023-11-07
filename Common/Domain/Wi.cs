using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Domain;
public class Wi
{
    [Key, StringLength(10), Column("tconst", TypeName = "char(10)")]
    public string Tconst { get; set; } = null!;

    [Column("word", TypeName = "character varying")]
    public string Word { get; set; } = null!;

    [StringLength(1), Column("field", TypeName = "char(1)")]
    public char Field { get; set; }
    
    [Column("lexeme", TypeName = "character varying")]
    public string? Lexeme { get; set; }
}