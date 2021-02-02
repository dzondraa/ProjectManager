using System;
using System.Collections.Generic;
using System.Text;

namespace AzureTableDataAccess.Entities
{
    public class Task : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public Task()
        {
        }

        // Partition id is project id - implicit one-to-many relationship 
        public Task(string projectId, string rowKey) : base(projectId, rowKey)
        {
            
        }

    }
}
