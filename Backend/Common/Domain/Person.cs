using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Common.Domain;

[Table("person")]
public class Person
{
    [Key, Column("id"), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; } = null!;

    [Required, StringLength(255), Column("name")]
    public string? Name { get; set; }
    
    [StringLength(4), Column("birth_year")]
    public string? BirthYear { get; set; }

    [StringLength(4), Column("death_year")]
    public string? DeathYear { get; set; }

    [StringLength(255), Column("professions")]
    public string? Professions { get; set; }

    [StringLength(255), Column("known_for")]
    public string? KnownFor { get; set; }
    public virtual ICollection<Role> Roles { get; set; }
    public virtual ICollection<BookmarkPersonality> BookmarkPersonalities { get; set; }
}