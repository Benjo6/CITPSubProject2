using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Domain;

public  class Role
{
    [Key, StringLength(10), Column("movie_id", TypeName = "char(10)", Order = 0)]
    public string? MovieId { get; set; }

    [Key, StringLength(10), Column("person_id", TypeName = "char(10)", Order = 1)]
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
    public Movie? Movie { get; set; }

    [ForeignKey("PersonId")]
    public Person? Person { get; set; }
}