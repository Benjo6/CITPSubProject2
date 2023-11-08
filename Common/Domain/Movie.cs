using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Common.Domain;

[Table("movie")]
public class Movie
{
    [Key, StringLength(10), Column("id", TypeName = "char(10)"), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; } = null!;

    [StringLength(255), Column("type")]
    public string? Type { get; set; }

    [StringLength(255), Column("title")]
    public string? Title { get; set; }

    [StringLength(255), Column("original_title")]
    public string? OriginalTitle { get; set; }

    [Column("is_adult")]
    public bool? IsAdult { get; set; }

    [StringLength(4), Column("start_year", TypeName = "char(4)")]
    public string? StartYear { get; set; }

    [StringLength(4), Column("end_year", TypeName = "char(4)")]
    public string? EndYear { get; set; }

    [Column("runtime")]
    public int? Runtime { get; set; }

    [StringLength(255), Column("genres")]
    public string? Genres { get; set; }

    [Column("rating", TypeName = "decimal(3, 1)")]
    public decimal? Rating { get; set; }

    [Column("votes")]
    public int? Votes { get; set; }
}