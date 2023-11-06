using Common.Domain;

namespace Common.DataTransferObjects;

public class RatingHistoryDTO
{
    public int Id { get; set; }
        
    public int UserId { get; set; }
    public string MovieId { get; set; } = null!;
    public int RatingValue { get; set; }
    public DateOnly RatingDate { get; set; }
    public virtual Movie Movie { get; set; } = null!;
    public virtual User User { get; set; } = null!;
}
    
public class CreateRatingHistoryDTO
{
        
    public int UserId { get; set; }
    public string MovieId { get; set; } = null!;
    public int RatingValue { get; set; }
    public DateOnly RatingDate { get; set; }
    public virtual Movie Movie { get; set; } = null!;
    public virtual User User { get; set; } = null!;
}
    
public class UpdateRatingHistoryDTO
{
    public int Id { get; set; }
        
    public int UserId { get; set; }
    public string MovieId { get; set; } = null!;
    public int RatingValue { get; set; }
    public DateOnly RatingDate { get; set; }
    public virtual Movie Movie { get; set; } = null!;
    public virtual User User { get; set; } = null!;
}
public record SimpleRatingHistory(string MovieId, int RatingValue);
