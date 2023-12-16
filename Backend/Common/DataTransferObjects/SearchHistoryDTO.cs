namespace Common.DataTransferObjects;

public class SearchHistoryDTO
{
    public string Id { get; set; }
    public string UserId { get; set; }
    public string? SearchQuery { get; set; }
    public DateOnly? SearchDate { get; set; }
}



public record SearchResult(string Id, string Title, decimal rating);

public record StructuredSearchResult(string Id, string Title);
