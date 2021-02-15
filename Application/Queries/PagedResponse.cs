using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Queries
{
    public class PagedResponse<T>
    {
        public int TotalCount { get; set; }
        public List<T> Items { get; set; }
    }
}
