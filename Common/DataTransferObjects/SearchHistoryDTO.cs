using Common.Domain;

namespace Common.DataTransferObjects;

public class SearchHistoryDTO
{
    public string Id { get; set; }
        
    public string? UserId { get; set; }
    public string SearchQuery { get; set; } = null!;
    public DateOnly SearchDate { get; set; }
    public virtual User? User { get; set; }
}



public record SearchResult(string Id, string Title, DateTimeOffset timestamp);