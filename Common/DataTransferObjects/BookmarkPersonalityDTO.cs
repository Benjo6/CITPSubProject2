using Common.Domain;

namespace Common.DataTransferObjects;

public class BookmarkPersonalityDTO
{
    public int UserId { get; set; }
    public string PersonId { get; set; } = null!;
    public DateOnly BookmarkDate { get; set; }
    public virtual Person Person { get; set; } = null!;
    public virtual User User { get; set; } = null!;
}
    
public class AlterBookmarkPersonalityDTO
{
    public int UserId { get; set; }
    public string PersonId { get; set; } = null!;
}
    
public class UpdateResponseBookmarkPersonalityDTO
{
    public int UserId { get; set; }
    public string PersonId { get; set; } = null!;
    public DateOnly BookmarkDate { get; set; }
}