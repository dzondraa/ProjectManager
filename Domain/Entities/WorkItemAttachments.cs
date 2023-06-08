using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class WorkItemAttachments
    {
        public int Id { get; set; }

        public int WorkItemId { get; set; }

        public int FileId { get; set; }


        public virtual File File { get; set; }

        public virtual WorkItem WorkItem { get; set; }
    }
}
