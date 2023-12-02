namespace Common.DataTransferObjects;

public class RatingHistoryDTO
{
    public int UserId { get; set; }
    public string MovieName { get; set; } = null!;
    public int RatingValue { get; set; }
    public DateOnly RatingDate { get; set; }
}
    
public class AlterRatingHistoryDTO
{
    public int UserId { get; set; }
    public string MovieId { get; set; } = null!;
    public int RatingValue { get; set; }
}

public class SimpleRatingHistory
{
    public string MovieId { get; set; } = null!;
    public int RatingValue { get; set; }
}