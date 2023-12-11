using Common.Domain;

namespace Common.DataTransferObjects;

public class GetAllMovieDTO
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string Type { get; set; }
    public int? Runtime { get; set; }
    public string? Genres { get; set; }
    public string? EndYear { get; set; }
    public bool? IsAdult { get; set; }
    public decimal? Rating { get; set; }
    public int EpisodesCount { get; set; }
}

public class GetOneMovieDTO
{
    public string OriginalTitle { get; set; }
    public string Type { get; set; }
    public int? Runtime { get; set; }
    public string? Genres { get; set; }
    public string? StartYear { get; set; }
    public string? EndYear { get; set; }
    public bool? IsAdult { get; set; }
    public decimal? Rating { get; set; }
    public int? Votes { get; set; }
    public ICollection<Alias> Aliases { get; set; }
    public ICollection<Episode> Episodes { get; set; }
}

public class AlterResponseMovieDTO
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string OriginalTitle { get; set; }
    public string Type { get; set; }
    public int? Runtime { get; set; }
    public string? Genres { get; set; }
    public string? StartYear { get; set; }
    public string? EndYear { get; set; }
    public bool? IsAdult { get; set; }
    public decimal? Rating { get; set; }
    public int? Votes { get; set; }
}

public class AlterMovieDTO
{
    public string Title { get; set; }
    public string OriginalTitle { get; set; }
    public string Type { get; set; }
    public int? Runtime { get; set; }
    public string? Genres { get; set; }
    public string? StartYear { get; set; }
    public string? EndYear { get; set; }
    public bool? IsAdult { get; set; }
    public decimal? Rating { get; set; }
    public int? Votes { get; set; }
}

public class SimilarMovie
{
    public string Id { get; set; }
    public string Title { get; set; }
}

public class BestMatch
{
    public string Tconst { get; set; }
    public long Rank { get; set; }
}

public class WordFrequency
{
    public string Word { get; set; }
    public long Frequency { get; set; }
}

public class SearchResults
{
    public string Id { get; set; }
    public string Title { get; set; }
}
