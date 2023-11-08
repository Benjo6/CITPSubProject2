namespace Common.DataTransferObjects;

public class BookmarkMovieDTO
{
    public string UserId { get; set; }
    public string AliasName { get; set; }
    public string? Note { get; set; }
    public DateOnly BookmarkDate { get; set; }

}
    
public class AlterBookmarkMovieDTO
{
    public string UserId { get; set; }
    public string AliasId { get; set; }
    public string? Note { get; set; }
}

