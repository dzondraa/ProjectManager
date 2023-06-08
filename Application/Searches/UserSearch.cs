using Api.Searches;

namespace Application.Searches
{
    public class UserSearch : PagedSearch
    {
        public string UserName { get; set; }
        
        public string Email { get; set; }
    
    }
}
