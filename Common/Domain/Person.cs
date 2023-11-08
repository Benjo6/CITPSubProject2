using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Common.Domain;

[Table("person")]
public class Person
{
    [Key, StringLength(10), Column("id", TypeName = "char(10)"), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; } = null!;

    [Required, StringLength(255), Column("name")]
    public string? Name { get; set; }

    [StringLength(4), Column("birth_year", TypeName = "char(4)")]
    public string? BirthYear { get; set; }

    [StringLength(4), Column("death_year", TypeName = "char(4)")]
    public string? DeathYear { get; set; }

    [StringLength(255), Column("professions")]
    public string? Professions { get; set; }

    [StringLength(255), Column("known_for")]
    public string? KnownFor { get; set; }
    public ICollection<Role> Roles { get; set; }
    public ICollection<BookmarkPersonality> BookmarkPersonalities { get; set; }
}