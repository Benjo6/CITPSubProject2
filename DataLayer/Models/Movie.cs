using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models;

[Table("movie")]
public class Movie
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public string Id { get; set; }

    [Column("type")]
    public string Type {  get; set; }

    [Column("title")]
    public string Title { get; set; }

    [Column("original_title")]
    public string OriginalTitle { get; set; }

    [Column("is_adult")]
    public bool IsAdult {  get; set; }

    [Column("start_year")]
    public DateTime? StartYear { get; set; }

    [Column("end_year")]
    public DateTime? EndYear { get; set; }

    [Column("runtime")]
    public int Runtime { get; set; }

    [Column("genres")]
    public string Genre { get; set; }

    [Column("rating")]
    public double Rating {  get; set; }

    [Column("votes")]
    public int Votes { get; set; }



}
