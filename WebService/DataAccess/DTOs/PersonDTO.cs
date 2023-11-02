using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; using SubProject2.Models;

namespace SubProject2.DataAccess.DTOs
{
    public class PersonDTO
    {
        public int Id { get; set; }
        
        public string? Name { get; set; }
        public string? BirthYear { get; set; }
        public string? DeathYear { get; set; }
        public string? Professions { get; set; }
        public string? KnownFor { get; set; }
        public virtual ICollection<Bookmarkpersonality> Bookmarkpersonalities { get; set; } = new List<Bookmarkpersonality>();
    }
    
    public class CreatePersonDTO
    {
        
        public string? Name { get; set; }
        public string? BirthYear { get; set; }
        public string? DeathYear { get; set; }
        public string? Professions { get; set; }
        public string? KnownFor { get; set; }
        public virtual ICollection<Bookmarkpersonality> Bookmarkpersonalities { get; set; } = new List<Bookmarkpersonality>();
    }
    
    public class UpdatePersonDTO
    {
        public int Id { get; set; }
        
        public string? Name { get; set; }
        public string? BirthYear { get; set; }
        public string? DeathYear { get; set; }
        public string? Professions { get; set; }
        public string? KnownFor { get; set; }
        public virtual ICollection<Bookmarkpersonality> Bookmarkpersonalities { get; set; } = new List<Bookmarkpersonality>();
    }
}

