using System.Collections;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class WorkItemType
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<WorkItem> WorkItems { get; set;}

    }
}
