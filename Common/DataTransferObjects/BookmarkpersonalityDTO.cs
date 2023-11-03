using Common.Domain;

namespace Common.DataTransferObjects
{
    public class BookmarkpersonalityDTO
    {
        public int Id { get; set; }
        
        public int UserId { get; set; }
        public string PersonId { get; set; } = null!;
        public DateOnly BookmarkDate { get; set; }
        public virtual Person Person { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
    
    public class CreateBookmarkpersonalityDTO
    {
        
        public int UserId { get; set; }
        public string PersonId { get; set; } = null!;
        public DateOnly BookmarkDate { get; set; }
        public virtual Person Person { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
    
    public class UpdateBookmarkpersonalityDTO
    {
        public int Id { get; set; }
        
        public int UserId { get; set; }
        public string PersonId { get; set; } = null!;
        public DateOnly BookmarkDate { get; set; }
        public virtual Person Person { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}

