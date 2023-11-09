using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Domain;

[Table("bookmarkpersonality")]
public class BookmarkPersonality
{
    [Key, StringLength(10), Column("user_id", TypeName = "varchar(10)", Order = 0)]
    public string UserId { get; set; }

    [Required]
    [Key, StringLength(10),Column("person_id", TypeName = "varchar(10)", Order = 1)]
    public string PersonId { get; set; } = null!;

    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    [Column("bookmark_date")]
    public DateOnly BookmarkDate { get; set; }

    [ForeignKey("PersonId")]
    public Person Person { get; set; } = null!;

    [ForeignKey("UserId")]
    public User User { get; set; } = null!;
}