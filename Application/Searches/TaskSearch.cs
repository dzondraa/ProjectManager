using Api.Searches;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Searches
{
    public class TaskSearch : PagedSearch
    {
        public string ProjectId { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public string Include { get; set; } = null;
    }
}
