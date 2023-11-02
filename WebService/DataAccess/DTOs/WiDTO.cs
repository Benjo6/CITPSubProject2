using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; using SubProject2.Models;

namespace SubProject2.DataAccess.DTOs
{
    public class WiDTO
    {
        public int Id { get; set; }
        
        public string Tconst { get; set; } = null!;
        public string Word { get; set; } = null!;
        public char Field { get; set; }
        public string? Lexeme { get; set; }
    }
    
    public class CreateWiDTO
    {
        
        public string Tconst { get; set; } = null!;
        public string Word { get; set; } = null!;
        public char Field { get; set; }
        public string? Lexeme { get; set; }
    }
    
    public class UpdateWiDTO
    {
        public int Id { get; set; }
        
        public string Tconst { get; set; } = null!;
        public string Word { get; set; } = null!;
        public char Field { get; set; }
        public string? Lexeme { get; set; }
    }
}

