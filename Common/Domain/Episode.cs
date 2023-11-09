using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Domain;

[Table("episode")]
public class Episode
{
    [Key, StringLength(10), Column("id", TypeName = "varchar(10)")]
    public string Id { get; set; } = null!;

    [StringLength(10), Column("series_id", TypeName = "varchar(10)")]
    public string? SeriesId { get; set; }

    [Column("season")]
    public int? Season { get; set; }

    [Column("episode")]
    public int? Episode1 { get; set; }

    [ForeignKey("SeriesId")]
    public virtual Movie? Series { get; set; }
}
