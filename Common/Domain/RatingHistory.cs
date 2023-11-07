using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Domain;

public class RatingHistory
{
    [Key, Column("user_id", Order = 0)]
    public int UserId { get; set; }

    [Key, StringLength(10), Column("movie_id", TypeName = "char(10)", Order = 1)]
    public string MovieId { get; set; } = null!;

    [Required, Column("rating_value")]
    public int RatingValue { get; set; }

    [Required, DatabaseGenerated(DatabaseGeneratedOption.Computed), Column("rating_date")]
    public DateOnly RatingDate { get; set; }

    [ForeignKey("MovieId")]
    public virtual Movie Movie { get; set; } = null!;

    [ForeignKey("UserId")]
    public virtual User User { get; set; } = null!;
}