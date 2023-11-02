using SubProject2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; using SubProject2.Models;

namespace SubProject2.DataAccess.DTOs
{
    public class BookmarkmovieDTO
    {
        public int Id { get; set; }
        
        public int UserId { get; set; }
        public int AliasId { get; set; }
        public DateOnly BookmarkDate { get; set; }
        public string? Note { get; set; }
        public virtual Alias Alias { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
    
    public class CreateBookmarkmovieDTO
    {
        
        public int UserId { get; set; }
        public int AliasId { get; set; }
        public DateOnly BookmarkDate { get; set; }
        public string? Note { get; set; }
        public virtual Alias Alias { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
    
    public class UpdateBookmarkmovieDTO
    {
        public int Id { get; set; }
        
        public int UserId { get; set; }
        public int AliasId { get; set; }
        public DateOnly BookmarkDate { get; set; }
        public string? Note { get; set; }
        public virtual Alias Alias { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}

