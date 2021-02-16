using AzureTableDataAccess.Entities;
using Microsoft.Azure.Cosmos.Table;
using Microsoft.Azure.Documents.SystemFunctions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureTableDataAccess
{
    public class TableCli
    {
        private readonly CloudStorageAccount _storageAcc;
        public CloudTable table;

        public TableCli(CloudStorageAccount account, string tableName)
        {
            _storageAcc = account;
            try
            {
                CloudTableClient tableClient = _storageAcc.CreateCloudTableClient(new TableClientConfiguration());
                table = tableClient.GetTableReference(tableName);
            }
            catch (Exception ex)
            {
                // Logg ex
            }
        }

        public bool EntityExists(string partitionId, string rowId)
        {

            var task = GetEntity<Project>(partitionId, rowId);
            task.Wait();
            var res = task.Result;
            return res == null ? false : true;
        }

        public async Task<TableEntity> InsertOrMergeEntityAsync(TableEntity entity)
        {
            TableOperation insertOrMergeOp = TableOperation.InsertOrMerge(entity);
            // Executing operation
            TableResult result = await table.ExecuteAsync(insertOrMergeOp);
            TableEntity insertedEntity = result.Result as TableEntity;
            return insertedEntity;
        }

        public async Task InsertDynamicEntity(DynamicTableEntity entity)
        {
            try
            {
                var insertOperation = TableOperation.Insert(entity);
                await table.ExecuteAsync(insertOperation);
            }
            catch (Exception ExceptionObj)
            {
                throw ExceptionObj;
            }
        }

        public async Task<TableEntity> MergeEntityAsync(TableEntity entity)
        {
            entity.ETag = "*";
            TableOperation mergeOperation = TableOperation.Merge(entity);
            // Executing operation
            TableResult result = await table.ExecuteAsync(mergeOperation);
            TableEntity mergedEntity = result.Result as TableEntity;
            return mergedEntity;
        }

        public async Task<TableEntity> MergeDynamicEntityAsync(DynamicTableEntity entity)
        {
            entity.ETag = "*";
            TableOperation mergeOperation = TableOperation.Replace(entity);
            // Executing operation
            TableResult result = await table.ExecuteAsync(mergeOperation);
            TableEntity deletedEntity = result.Result as TableEntity;
            return deletedEntity;
        }

        public async Task<Entity> GetSingleEntity<T>(string partitionKey, string rowKey) where T : Entity
        {
            return await GetEntity<T>(partitionKey, rowKey) as Entity;
        }

        public async Task<DynamicTableEntity> GetSingleDynamicEntity(string partitionKey, string rowKey)
        {
            TableOperation retrieveOperation = TableOperation.Retrieve(partitionKey, rowKey);
            TableResult result = await table.ExecuteAsync(retrieveOperation);
            if (result.HttpStatusCode == 404) throw new Exception("not exist");
            DynamicTableEntity entity = result.Result as DynamicTableEntity;
            return entity;
        }


        private async Task<Entity> GetEntity<T>(string partitionKey, string rowKey) where T : Entity
        {
            TableOperation retrieveOperation = TableOperation.Retrieve<T>(partitionKey, rowKey);
            TableResult result = await table.ExecuteAsync(retrieveOperation);
            if (result.HttpStatusCode == 404) throw new Exception("not exist");
            Entity entity = result.Result as Entity;
            if (entity.Deleted) throw new Exception("Resource was deleted");
            return entity;
        }

        //public async Task<Entity> GetEntities<T>() where T : TableEntity
        //{
        //    IQueryable<Project> query;
        //    query = table.CreateQuery<Project>();
        //    return await GetEntity<T>(partitionKey, rowKey) as Entity;
        //}


    }
}
