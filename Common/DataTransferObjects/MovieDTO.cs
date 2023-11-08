using Common.Domain;

namespace Common.DataTransferObjects;

public class GetAllMovieDTO
{
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

public class AddAndUpdateMovieDTO
{
    public int Id { get; set; }
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
    public ICollection<Alias> Aliases { get; set; }
    public ICollection<Episode> Episodes { get; set; }
    public ICollection<RatingHistory> RatingHistories { get; set; }
}

public class CreateMovieDTO
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
    public ICollection<Alias> Aliases { get; set; }
    public ICollection<Episode> Episodes { get; set; }
    public ICollection<RatingHistory> RatingHistories { get; set; }
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
