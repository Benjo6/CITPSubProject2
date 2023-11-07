using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Domain;

public class BookmarkMovie
{
    [Key, Column("user_id", Order = 0)]
    public int UserId { get; set; }

    [Key, Column("alias_id", Order = 1)]
    public int AliasId { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    [Column("bookmark_date")]
    public DateOnly BookmarkDate { get; set; }

    [StringLength(255)]
    [Column("note")]
    public string? Note { get; set; }

    [ForeignKey("AliasId")]
    public Alias Alias { get; set; } = null!;

    [ForeignKey("UserId")]
    public User User { get; set; } = null!;
}
