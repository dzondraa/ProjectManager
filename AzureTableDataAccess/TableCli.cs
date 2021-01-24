using Microsoft.Azure.Cosmos.Table;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AzureTableDataAccess
{
    public class TableCli
    {
        private readonly CloudStorageAccount _storageAcc;
        private readonly CloudTable _table;

        public TableCli(CloudStorageAccount account, String tableName)
        {
            _storageAcc = account;
            try
            {
                CloudTableClient tableClient = _storageAcc.CreateCloudTableClient(new TableClientConfiguration());
                _table = tableClient.GetTableReference(tableName);
            }
            catch (Exception ex)
            {
                // Logg ex
            }
        }

        public async Task<TableEntity> InsertOrMergeEntityAsync(TableEntity entity)
        {
            TableOperation insertOrMergeOp = TableOperation.InsertOrMerge(entity);
            // Executing operation
            TableResult result = await _table.ExecuteAsync(insertOrMergeOp);
            TableEntity insertedEntity = result.Result as TableEntity;
            return insertedEntity;
        }

        public async Task<TableEntity> MergeEntityAsync(DynamicTableEntity entity)
        {
            entity.ETag = "*";
            TableOperation insertOrMergeOp = TableOperation.Replace(entity);
            // Executing operation
            TableResult result = await _table.ExecuteAsync(insertOrMergeOp);
            TableEntity insertedEntity = result.Result as TableEntity;
            return insertedEntity;
        }

        public async Task<TableEntity> QueryEntity<T>(string partitionKey, string rowKey) where T : ITableEntity
        {
            //TableOperation retrieveOperation = TableOperation.Retrieve<T>(partitionKey, rowKey);

            //TableResult result = await _table.ExecuteAsync(retrieveOperation);
            //TableEntity entity = result.Result as TableEntity;
            return await GetEntity<T>(partitionKey, rowKey);
        }

        //public async Task DeleteEntityAsync<T>(string partitionKey, string rowKey) where T : ITableEntity
        //{
    
            


        //}

        private async Task<TableEntity> GetEntity<T>(string partitionKey, string rowKey) where T : ITableEntity
        {
            TableOperation retrieveOperation = TableOperation.Retrieve<T>(partitionKey, rowKey);
            TableResult result = await _table.ExecuteAsync(retrieveOperation);
            TableEntity entity = result.Result as TableEntity;
            return entity;
        }


    }
}
