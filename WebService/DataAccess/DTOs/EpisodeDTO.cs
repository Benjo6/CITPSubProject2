using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; using SubProject2.Models;

namespace SubProject2.DataAccess.DTOs
{
    public class EpisodeDTO
    {
        public int Id { get; set; }
        
        public string? SeriesId { get; set; }
        public int? Season { get; set; }
        public int? Episode1 { get; set; }
        public virtual Movie? Series { get; set; }
    }
    
    public class CreateEpisodeDTO
    {
        
        public string? SeriesId { get; set; }
        public int? Season { get; set; }
        public int? Episode1 { get; set; }
        public virtual Movie? Series { get; set; }
    }
    
    public class UpdateEpisodeDTO
    {
        public int Id { get; set; }
        
        public string? SeriesId { get; set; }
        public int? Season { get; set; }
        public int? Episode1 { get; set; }
        public virtual Movie? Series { get; set; }
    }
}

