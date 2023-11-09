using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Domain;

[Table("bookmarkmovie")]
public class BookmarkMovie
{
    [Key, StringLength(10), Column("user_id", TypeName = "varchar(10)", Order = 0)]
    public string UserId { get; set; } = null!;
    
    [Key, StringLength(10), Column("alias_id", TypeName = "varchar(10)", Order = 1)] 
    public string AliasId { get; set; }

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
