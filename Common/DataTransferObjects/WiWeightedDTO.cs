using Common.Domain;

namespace Common.DataTransferObjects
{
    public class WiWeightedDTO
    {
        public int Id { get; set; }
        
        public string? Tconst { get; set; }
        public string? Word { get; set; }
        public char? Field { get; set; }
        public string? Lexeme { get; set; }
        public double? Weight { get; set; }
        public virtual Movie? TconstNavigation { get; set; }
    }
    
    public class CreateWiWeightedDTO
    {
        
        public string? Tconst { get; set; }
        public string? Word { get; set; }
        public char? Field { get; set; }
        public string? Lexeme { get; set; }
        public double? Weight { get; set; }
        public virtual Movie? TconstNavigation { get; set; }
    }
    
    public class UpdateWiWeightedDTO
    {
        public int Id { get; set; }
        
        public string? Tconst { get; set; }
        public string? Word { get; set; }
        public char? Field { get; set; }
        public string? Lexeme { get; set; }
        public double? Weight { get; set; }
        public virtual Movie? TconstNavigation { get; set; }
    }
}

