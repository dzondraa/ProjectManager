using Microsoft.Azure.Cosmos.Table;
using System;
using System.Collections.Generic;
using System.Text;

namespace AzureTableDataAccess
{
    public abstract class Entity : TableEntity
    {
     
        // Soft delete option
        public DateTime? DeletedAt { get; set; }

        public Entity(string partitionKey, string rowKey)
        {
            PartitionKey = partitionKey;
            RowKey = rowKey;
        }
    }

 
}
