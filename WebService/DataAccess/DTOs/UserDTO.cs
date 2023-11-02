using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; using SubProject2.Models;

namespace SubProject2.DataAccess.DTOs
{
    public class UserDTO
    {
        public int Id { get; set; }
        
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public DateOnly RegistrationDate { get; set; }
        public bool? Isadmin { get; set; }
        public virtual ICollection<Bookmarkmovie> Bookmarkmovies { get; set; } = new List<Bookmarkmovie>();
        public virtual ICollection<Bookmarkpersonality> Bookmarkpersonalities { get; set; } = new List<Bookmarkpersonality>();
        public virtual ICollection<Ratinghistory> Ratinghistories { get; set; } = new List<Ratinghistory>();
        public virtual ICollection<Searchhistory> Searchhistories { get; set; } = new List<Searchhistory>();
    }
    
    public class CreateUserDTO
    {
        
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public DateOnly RegistrationDate { get; set; }
        public bool? Isadmin { get; set; }
        public virtual ICollection<Bookmarkmovie> Bookmarkmovies { get; set; } = new List<Bookmarkmovie>();
        public virtual ICollection<Bookmarkpersonality> Bookmarkpersonalities { get; set; } = new List<Bookmarkpersonality>();
        public virtual ICollection<Ratinghistory> Ratinghistories { get; set; } = new List<Ratinghistory>();
        public virtual ICollection<Searchhistory> Searchhistories { get; set; } = new List<Searchhistory>();
    }
    
    public class UpdateUserDTO
    {
        public int Id { get; set; }
        
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public DateOnly RegistrationDate { get; set; }
        public bool? Isadmin { get; set; }
        public virtual ICollection<Bookmarkmovie> Bookmarkmovies { get; set; } = new List<Bookmarkmovie>();
        public virtual ICollection<Bookmarkpersonality> Bookmarkpersonalities { get; set; } = new List<Bookmarkpersonality>();
        public virtual ICollection<Ratinghistory> Ratinghistories { get; set; } = new List<Ratinghistory>();
        public virtual ICollection<Searchhistory> Searchhistories { get; set; } = new List<Searchhistory>();
    }
}

