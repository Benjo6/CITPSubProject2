﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Domain;

[Table("alias")]
public class Alias
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public string Id { get; set; }

    [Column("movie_id")]
    public string? MovieId { get; set; }

    [Column("ordering")]
    public int? Ordering { get; set; }

    [StringLength(255)]
    [Column("title")]
    public string? Title { get; set; }

    [StringLength(255)]
    [Column("region")]
    public string? Region { get; set; }

    [StringLength(255)]
    [Column("language")]
    public string? Language { get; set; }

    [StringLength(255)]
    [Column("types")]
    public string? Types { get; set; }

    [StringLength(255)]
    [Column("attributes")]
    public string? Attributes { get; set; }

    [Column("is_original")]
    public bool? IsOriginal { get; set; }

    
    [ForeignKey("MovieId")]
    public virtual Movie? Movie { get; set; }
}
