using Common.Domain;

namespace Common.DataTransferObjects;

public class AliasDTO
{
    public string Id { get; set; }
    public virtual Movie? Movie { get; set; }
    public int? Ordering { get; set; }
    public string? Title { get; set; }
    public string? Region { get; set; }
    public string? Language { get; set; }
    public string? Types { get; set; }
    public string? Attributes { get; set; }
    public bool? IsOriginal { get; set; }
    public virtual ICollection<BookmarkMovie> Bookmarkmovies { get; set; } = new List<BookmarkMovie>();
}
    
public class AlterAliasDTO
{
    public string? MovieId { get; set; }
    public int? Ordering { get; set; }
    public string? Title { get; set; }
    public string? Region { get; set; }
    public string? Language { get; set; }
    public string? Types { get; set; }
    public string? Attributes { get; set; }
    public bool? IsOriginal { get; set; }
}
    