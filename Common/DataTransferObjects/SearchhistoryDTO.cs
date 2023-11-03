using Common.Domain;

namespace Common.DataTransferObjects
{
    public class SearchhistoryDTO
    {
        public int Id { get; set; }
        
        public int? UserId { get; set; }
        public string SearchQuery { get; set; } = null!;
        public DateOnly SearchDate { get; set; }
        public virtual User? User { get; set; }
    }
    
    public class CreateSearchhistoryDTO
    {
        
        public int? UserId { get; set; }
        public string SearchQuery { get; set; } = null!;
        public DateOnly SearchDate { get; set; }
        public virtual User? User { get; set; }
    }
    
    public class UpdateSearchhistoryDTO
    {
        public int Id { get; set; }
        
        public int? UserId { get; set; }
        public string SearchQuery { get; set; } = null!;
        public DateOnly SearchDate { get; set; }
        public virtual User? User { get; set; }
    }
}

