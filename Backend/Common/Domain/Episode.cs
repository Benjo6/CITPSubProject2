using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Domain;

[Table("episode")]
public class Episode
{
    [Key, StringLength(50), Column("id"), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string? Id { get; set; }

    [StringLength(50), Column("series_id")]
    public string? SeriesId { get; set; }

    [Column("season")]
    public int? Season { get; set; }

    [Column("episode")]
    public int? Episode1 { get; set; }

    [ForeignKey("SeriesId")]
    public virtual Movie? Series { get; set; }
    
}
