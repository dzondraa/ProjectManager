using Api.Searches;

namespace Application.Searches
{
    public class CommentSearch : PagedSearch
    {
        public string Content { get; set; }

        public int? UserId { get; set; }

        public int? ParentID { get; set; }
    }
}
