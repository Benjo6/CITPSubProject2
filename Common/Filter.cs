namespace Common;

public class Filter
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public string SortBy { get; set; }
    public bool IsAscending { get; set; } 
    public IEnumerable<FilterCondition> Conditions { get; set; }
    public Filter()
    {
        Conditions = new List<FilterCondition>();
        SortBy = string.Empty;
        IsAscending = true;
        PageNumber = 1;
        PageSize = 10;
    }
}

public class FilterCondition
{
    public string PropertyName { get; set; }
    public string Value { get; set; }
    public OperatorEnum Operator { get; set; }
}

public enum OperatorEnum
{
    Equals,
    Less,
    LessOrEqual,
    Greater,
    GreaterOrEqual
}
