namespace Common;

public class Filter
{
    public int PageNumber { get; }
    public int PageSize { get; }
    public string SortBy { get; }
    public bool IsAscending { get; } 
    public IEnumerable<FilterCondition> Conditions { get; }

    public Filter(int pageNumber, int pageSize, string sortBy, bool isAscending,
        IEnumerable<FilterCondition>? conditions)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
        SortBy = sortBy;
        IsAscending = isAscending;
        Conditions = conditions ?? new List<FilterCondition>();
    }
    
    // For the tests
    public Filter()
    {
        PageNumber = 1;
        PageSize = 10;
        SortBy = "Id";
        IsAscending =  true;
        Conditions = new List<FilterCondition>();
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
