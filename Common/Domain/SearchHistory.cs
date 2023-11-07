using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Domain;

public class SearchHistory
{
    [Key, Column("id")]
    public int Id { get; set; }

    [Required, Column("user_id")]
    public int? UserId { get; set; }

    [Required, StringLength(255), Column("search_query")]
    public string SearchQuery { get; set; } = null!;

    [Required, DatabaseGenerated(DatabaseGeneratedOption.Computed), Column("search_date")]
    public DateOnly SearchDate { get; set; }

    [ForeignKey("UserId")]
    public User? User { get; set; }
}