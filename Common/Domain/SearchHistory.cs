using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Domain;

[Table("searchhistory")]
public class SearchHistory
{
    [Key, StringLength(10), Column("id", TypeName = "char(10)"), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; }

    [Required, Column("user_id")]
    public string? UserId { get; set; }

    [Required, StringLength(255), Column("search_query")]
    public string SearchQuery { get; set; } = null!;

    [Required, DatabaseGenerated(DatabaseGeneratedOption.Computed), Column("search_date")]
    public DateOnly SearchDate { get; set; }

    [ForeignKey("UserId")]
    public User? User { get; set; }
}