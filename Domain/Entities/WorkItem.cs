using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class WorkItem
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }


        //public IEnumerable<Dictionary<string, object>> Properties { get; set; } = null;

        public virtual Project Project { get; set; }

        public virtual WorkItemType WorkItemType { get; set; }

        public virtual ICollection<WorkItemAttachments> Attachments { get;set; }

    }
}
