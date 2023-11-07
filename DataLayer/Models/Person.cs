using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models;

public class Person
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public string Id { get; set; }

    [Column("name")]
    public string Name { get; set; }

    [Column("birth_year")]
    public DateTime? BirthDate { get; set;}

    [Column("death_year")]
    public DateTime? DeathDate { get; set; }

    [Column("professions")]
    public string Profession {  get; set; }

    [Column("known_for")]
    public string KnownFor {  get; set; }
}
