using Application.Commands;
using Application.DataTransfer;
using AzureTableDataAccess;
using AzureTableDataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Commands
{
    public class TableCliCreateTaskAsync : ICreateTaskCommandAsync
    {
        private readonly TableCli _tableCli;

        public TableCliCreateTaskAsync()
        {
            _tableCli = new TableCli(AzureStorageConnection.Instance(), "Tasks");
        }

        public int Id => 221;

        public string Name => "Create task asyncronously";

        public async Task Execute(TaskDto request)
        {
            var date = DateTime.Now;
            await _tableCli.InsertOrMergeEntityAsync(new Tasks 
            {
                PartitionKey = request.ProjectId,
                RowKey = Guid.NewGuid().ToString(),
                Description = request.Description,
                Name = request.Name
            });
        }
    }
}
