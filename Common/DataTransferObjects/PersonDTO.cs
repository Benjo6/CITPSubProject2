using Common.Domain;

namespace Common.DataTransferObjects;

public class PersonDTO
{
    public int Id { get; set; }
        
    public string? Name { get; set; }
    public string? BirthYear { get; set; }
    public string? DeathYear { get; set; }
    public string? Professions { get; set; }
    public string? KnownFor { get; set; }
    public virtual ICollection<BookmarkPersonality> Bookmarkpersonalities { get; set; } = new List<BookmarkPersonality>();
}
    
public class CreatePersonDTO
{
        
    public string? Name { get; set; }
    public string? BirthYear { get; set; }
    public string? DeathYear { get; set; }
    public string? Professions { get; set; }
    public string? KnownFor { get; set; }
    public virtual ICollection<BookmarkPersonality> Bookmarkpersonalities { get; set; } = new List<BookmarkPersonality>();
}
    
public class UpdatePersonDTO
{
    public int Id { get; set; }
        
    public string? Name { get; set; }
    public string? BirthYear { get; set; }
    public string? DeathYear { get; set; }
    public string? Professions { get; set; }
    public string? KnownFor { get; set; }
    public virtual ICollection<BookmarkPersonality> Bookmarkpersonalities { get; set; } = new List<BookmarkPersonality>();
}

public record ActorBy(string Id, string Name);
public record PopularActor(string ActorName, decimal AverageRating);
public record PopularCoPlayer(string CoActorName, decimal AverageRating);
public record PersonWord(string Word, long Frequency);
