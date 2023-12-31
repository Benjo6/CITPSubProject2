namespace Common.DataTransferObjects;

public class GetAllPersonDTO
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public string? Professions { get; set; }
    public string? KnownFor { get; set; }
}

public class GetOnePersonDTO
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public string? BirthYear { get; set; }
    public string? DeathYear { get; set; }
    public string? Professions { get; set; }
    public string? KnownFor { get; set; }
}

public class UpdatePersonDTO
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public string? BirthYear { get; set; }
    public string? DeathYear { get; set; }
    public string? Professions { get; set; }
    public string? KnownFor { get; set; }
}

public class AlterPersonDTO
{
    public string? Name { get; set; }
    public string? BirthYear { get; set; }
    public string? DeathYear { get; set; }
    public string? Professions { get; set; }
    public string? KnownFor { get; set; }
}

public class ActorBy
{
    public string Id { get; set; }
    public string Name { get; set; }

}

public class PopularActor
{
    public string ActorName { get; set; }
    public decimal AverageRating { get; set; }
}

public class PopularCoPlayer
{
    public string CoActorName { get; set; }
    public decimal AverageRating { get; set; }
}

public class PersonWord
{
    public string Word { get; set; }
    public long Frequency { get; set; }
}
