namespace Common.DataTransferObjects;

public class BookmarkMovieDTO
{
    public string UserId { get; set; }
    public string MovieId { get; set; }
    public string? Note { get; set; }
    public DateOnly BookmarkDate { get; set; }
    public string MovieTitle { get; set; }
    public string MovieRating { get; set;}

}

public class AlterBookmarkMovieDTO
{  
    public string UserId { get; set; }
    public string MovieId { get; set; }
}

