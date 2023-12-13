using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Domain;

[Table("role")]
public class Role
{
    [Key, StringLength(50), Column("movie_id", Order = 0)]
    public string? MovieId { get; set; }

    [Key, StringLength(50), Column("person_id", Order = 1)]
    public string? PersonId { get; set; }

    [Column("ordering")]
    public int? Ordering { get; set; }

    [StringLength(255), Column("category")]
    public string? Category { get; set; }

    [StringLength(255), Column("job")]
    public string? Job { get; set; }

    [StringLength(255), Column("characters")]
    public string? Characters { get; set; }

    [ForeignKey("MovieId")]
    public virtual Movie? Movie { get; set; }

    [ForeignKey("PersonId")]
    public virtual Person? Person { get; set; }
}