using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Domain;

[Table("ratinghistory")]
public class RatingHistory
{
    [Key, StringLength(50), Column("user_id", Order = 0)]
    public string UserId { get; set; }

    [Key, StringLength(50), Column("movie_id", Order = 1)]
    public string MovieId { get; set; } = null!;

    [Required, Column("rating_value")]
    public int RatingValue { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Computed), Column("rating_date")]
    public DateOnly? RatingDate { get; set; }

    [ForeignKey("MovieId")]
    public virtual Movie Movie { get; set; } = null!;

    [ForeignKey("UserId")]
    public virtual User User { get; set; } = null!;
}