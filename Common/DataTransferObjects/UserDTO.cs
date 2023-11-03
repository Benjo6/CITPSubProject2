using Common.Domain;

namespace Common.DataTransferObjects
{
    public class UserDTO
    {
        public int Id { get; set; }
        
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public DateOnly RegistrationDate { get; set; }
        public bool? Isadmin { get; set; }
        public virtual ICollection<BookmarkMovie> Bookmarkmovies { get; set; } = new List<BookmarkMovie>();
        public virtual ICollection<BookmarkPersonality> Bookmarkpersonalities { get; set; } = new List<BookmarkPersonality>();
        public virtual ICollection<RatingHistory> Ratinghistories { get; set; } = new List<RatingHistory>();
        public virtual ICollection<SearchHistory> Searchhistories { get; set; } = new List<SearchHistory>();
    }
    
    public class CreateUserDTO
    {
        
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public DateOnly RegistrationDate { get; set; }
        public bool? Isadmin { get; set; }
        public virtual ICollection<BookmarkMovie> Bookmarkmovies { get; set; } = new List<BookmarkMovie>();
        public virtual ICollection<BookmarkPersonality> Bookmarkpersonalities { get; set; } = new List<BookmarkPersonality>();
        public virtual ICollection<RatingHistory> Ratinghistories { get; set; } = new List<RatingHistory>();
        public virtual ICollection<SearchHistory> Searchhistories { get; set; } = new List<SearchHistory>();
    }
    
    public class UpdateUserDTO
    {
        public int Id { get; set; }
        
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public DateOnly RegistrationDate { get; set; }
        public bool? Isadmin { get; set; }
        public virtual ICollection<BookmarkMovie> Bookmarkmovies { get; set; } = new List<BookmarkMovie>();
        public virtual ICollection<BookmarkPersonality> Bookmarkpersonalities { get; set; } = new List<BookmarkPersonality>();
        public virtual ICollection<RatingHistory> Ratinghistories { get; set; } = new List<RatingHistory>();
        public virtual ICollection<SearchHistory> Searchhistories { get; set; } = new List<SearchHistory>();
    }
}

