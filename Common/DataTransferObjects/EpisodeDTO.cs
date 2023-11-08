using Common.Domain;

namespace Common.DataTransferObjects;

public class EpisodeDTO
{
    public int Id { get; set; }
    public string? SeriesName { get; set; }
    public int? Season { get; set; }
    public int? Episode1 { get; set; }
}
    
public class AlterEpisodeDTO
{
        
    public string? SeriesId { get; set; }
    public int? Season { get; set; }
    public int? Episode1 { get; set; }
}
    