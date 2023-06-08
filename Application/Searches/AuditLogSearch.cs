using Api.Searches;
using System;

namespace Application.Searches
{
    public class AuditLogSearch : PagedSearch
    {
        public string? Action { get; set; }

        public DateTime? Timestamp { get; set; }

        public string? Actor { get; set; }
    }
}
