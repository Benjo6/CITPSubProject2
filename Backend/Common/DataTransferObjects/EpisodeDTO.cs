using Common.Domain;

namespace Common.DataTransferObjects;

public class EpisodeDTO
{
    public string Id { get; set; }
    public string? SeriesId { get; set; }
    public int? Season { get; set; }
    public int? Episode1 { get; set; }
}
    
public class AlterEpisodeDTO
{
        
    public string? SeriesId { get; set; }
    public int? Season { get; set; }
    public int? Episode1 { get; set; }
}
    