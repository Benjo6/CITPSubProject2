using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Domain;

public class BookmarkPersonality
{
    [Key, Column("user_id", Order = 0)]
    public int UserId { get; set; }

    [Required]
    [Key, Column("person_id", TypeName = "char(10)", Order = 1)]
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