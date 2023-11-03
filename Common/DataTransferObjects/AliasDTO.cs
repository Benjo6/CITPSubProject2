using Common.Domain;

namespace Common.DataTransferObjects
{
    public class AliasDTO
    {
        public int Id { get; set; }
        
        public string? MovieId { get; set; }
        public int? Ordering { get; set; }
        public string? Title { get; set; }
        public string? Region { get; set; }
        public string? Language { get; set; }
        public string? Types { get; set; }
        public string? Attributes { get; set; }
        public bool? IsOriginal { get; set; }
        public virtual ICollection<BookmarkMovie> Bookmarkmovies { get; set; } = new List<BookmarkMovie>();
        public virtual Movie? Movie { get; set; }
    }
    
    public class CreateAliasDTO
    {
        
        public string? MovieId { get; set; }
        public int? Ordering { get; set; }
        public string? Title { get; set; }
        public string? Region { get; set; }
        public string? Language { get; set; }
        public string? Types { get; set; }
        public string? Attributes { get; set; }
        public bool? IsOriginal { get; set; }
        public virtual ICollection<BookmarkMovie> Bookmarkmovies { get; set; } = new List<BookmarkMovie>();
        public virtual Movie? Movie { get; set; }
    }
    
    public class UpdateAliasDTO
    {
        public int Id { get; set; }
        
        public string? MovieId { get; set; }
        public int? Ordering { get; set; }
        public string? Title { get; set; }
        public string? Region { get; set; }
        public string? Language { get; set; }
        public string? Types { get; set; }
        public string? Attributes { get; set; }
        public bool? IsOriginal { get; set; }
        public virtual ICollection<BookmarkMovie> Bookmarkmovies { get; set; } = new List<BookmarkMovie>();
        public virtual Movie? Movie { get; set; }
    }
}

