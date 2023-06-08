namespace Api.Searches
{
    public abstract class PagedSearch
    {
        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 10;
    }
}
