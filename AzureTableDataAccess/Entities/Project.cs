using System;
using System.Collections.Generic;
using System.Text;

namespace AzureTableDataAccess.Entities
{
    public class Project : Entity
    {
        public string Name { get; set; }

        public Project(string partitionKey, string rowKey, string name) : base (partitionKey, rowKey)
        {
            Name = name;
        }
    }
}
