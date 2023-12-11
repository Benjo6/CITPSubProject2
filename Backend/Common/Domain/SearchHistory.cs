using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Domain;

[Table("searchhistory")]
public class SearchHistory
{
    [Key, Column("id"), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; }

    [Required, Column("user_id")]
    public string? UserId { get; set; }

    [Required, StringLength(255), Column("search_query")]
    public string SearchQuery { get; set; } = null!;

    [DatabaseGenerated(DatabaseGeneratedOption.Computed), Column("search_date")]
    public DateOnly? SearchDate { get; set; }

    [ForeignKey("UserId")]
    public virtual User? User { get; set; }
    
}