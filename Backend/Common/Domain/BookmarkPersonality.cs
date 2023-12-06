using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Domain;

[Table("bookmarkpersonality")]
public class BookmarkPersonality
{
    [Key, StringLength(50), Column("user_id", Order = 0)]
    public string UserId { get; set; }

    [Required]
    [Key, StringLength(50),Column("person_id", Order = 1)]
    public string PersonId { get; set; } = null!;

    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    [Column("bookmark_date")]
    public DateOnly? BookmarkDate { get; set; }

    [ForeignKey("PersonId")]
    public virtual Person Person { get; set; } = null!;

    [ForeignKey("UserId")]
    public virtual User User { get; set; } = null!;
}