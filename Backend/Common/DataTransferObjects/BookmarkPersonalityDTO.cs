using Common.Domain;

namespace Common.DataTransferObjects;

public class BookmarkPersonalityDTO
{
    public string UserId { get; set; } = null!;
    public string PersonId { get; set; } = null!;
    public DateOnly BookmarkDate { get; set; }
    public virtual Person Person { get; set; } = null!;
    public virtual User User { get; set; } = null!;
}
    
public class AlterBookmarkPersonalityDTO
{
    public string UserId { get; } = null!;
    public string PersonId { get; } = null!;
}
    
public class UpdateResponseBookmarkPersonalityDTO
{
    public string UserId { get; set; } = null!;
    public string PersonId { get; set; } = null!;
    public DateOnly BookmarkDate { get; set; }
}