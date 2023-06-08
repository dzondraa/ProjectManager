using System.Collections.Generic;

namespace Domain.Entities
{
    public class Project
    {
        public int Id { get; set; }

        public string Name { get; set; }

        //  Storing only path | guid of project code files
        public string Code { get; set; }

        public virtual ICollection<WorkItem> WorkItems { get; set; }

    }
}
