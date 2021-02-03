using System;
using System.Collections.Generic;
using System.Text;

namespace AzureTableDataAccess.Entities
{
    public class Tasks : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public Tasks()
        {
        }

        // Partition id is project id - implicit one-to-many relationship 
        public Tasks(string projectId, string rowKey) : base(projectId, rowKey)
        {
            
        }

    }
}
