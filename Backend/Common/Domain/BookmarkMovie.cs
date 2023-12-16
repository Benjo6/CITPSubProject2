using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Domain;

[Table("bookmarkmovie")]
public class BookmarkMovie
{
    [Key, Column("user_id", Order = 0)]
    public string UserId { get; set; } = null!;
    
    [Key, Column("movie_id", Order = 1)] 
    public string MovieId { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    [Column("bookmark_date")]
    public DateOnly? BookmarkDate { get; set; }

    [StringLength(255)]
    [Column("note")]
    public string? Note { get; set; }

    [ForeignKey("MovieId")]
    public virtual Movie Movie { get; set; } = null!;

    [ForeignKey("UserId")]
    public virtual User User { get; set; } = null!;
}
