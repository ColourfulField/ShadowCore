namespace ShadowTools.Utilities.Models
{
    public class BaseFilter
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string SortField { get; set; }
        public bool IsSortDescending { get; set; }

    }
}
