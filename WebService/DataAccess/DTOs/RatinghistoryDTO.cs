using SubProject2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; using SubProject2.Models;

namespace SubProject2.DataAccess.DTOs
{
    public class RatinghistoryDTO
    {
        public int Id { get; set; }
        
        public int UserId { get; set; }
        public string MovieId { get; set; } = null!;
        public int RatingValue { get; set; }
        public DateOnly RatingDate { get; set; }
        public virtual Movie Movie { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
    
    public class CreateRatinghistoryDTO
    {
        
        public int UserId { get; set; }
        public string MovieId { get; set; } = null!;
        public int RatingValue { get; set; }
        public DateOnly RatingDate { get; set; }
        public virtual Movie Movie { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
    
    public class UpdateRatinghistoryDTO
    {
        public int Id { get; set; }
        
        public int UserId { get; set; }
        public string MovieId { get; set; } = null!;
        public int RatingValue { get; set; }
        public DateOnly RatingDate { get; set; }
        public virtual Movie Movie { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}

