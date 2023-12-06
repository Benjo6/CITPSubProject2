namespace Common;

public class Filter
{
    public int PageNumber { get; }
    public int PageSize { get; }
    public string SortBy { get; }
    public bool IsAscending { get; }
    public Dictionary<string, string> FilterCriteria { get; } 

    public Filter(int pageNumber, int pageSize, string sortBy, bool isAscending, Dictionary<string, string> filterCriteria)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
        SortBy = sortBy;
        IsAscending = isAscending;
        FilterCriteria = filterCriteria; 
    }

    // For testing
    public Filter()
    {
        PageNumber = 1;
        PageSize = 10;
        SortBy = "Id";
        IsAscending = true;
        FilterCriteria = new Dictionary<string, string>();
    }
}