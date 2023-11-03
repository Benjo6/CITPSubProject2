using Common.Domain;

namespace Common.DataTransferObjects
{
    public class EpisodeDTO
    {
        public int Id { get; set; }
        
        public string? SeriesId { get; set; }
        public int? Season { get; set; }
        public int? Episode1 { get; set; }
        public virtual Movie? Series { get; set; }
    }
    
    public class CreateEpisodeDTO
    {
        
        public string? SeriesId { get; set; }
        public int? Season { get; set; }
        public int? Episode1 { get; set; }
        public virtual Movie? Series { get; set; }
    }
    
    public class UpdateEpisodeDTO
    {
        public int Id { get; set; }
        
        public string? SeriesId { get; set; }
        public int? Season { get; set; }
        public int? Episode1 { get; set; }
        public virtual Movie? Series { get; set; }
    }
}

