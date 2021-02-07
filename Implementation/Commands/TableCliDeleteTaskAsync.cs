using Application.Commands;
using Application.DataTransfer;
using AutoMapper;
using AzureTableDataAccess;
using AzureTableDataAccess.Entities;
using Microsoft.Azure.Cosmos.Table;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Commands
{
    public class TableCliDeleteTaskAsync : IDeleteTaskAsync
    {
        private readonly TableCli _tableCli;
        private readonly IMapper _mapper;
        public int Id => 2;

        public string Name => "Async deleting task using TableCli";

        public TableCliDeleteTaskAsync(IMapper mapper)
        {
            _mapper = mapper;
            _tableCli = new TableCli(AzureStorageConnection.Instance(), "Tasks");
        }

        public async Task Execute(TaskDto request)
        {
            // Mapping to dto to entity object - compatibile for storage operations
            var tableEntity = _mapper.Map<Tasks>(request);
            var dynTableEntity = new DynamicTableEntity(tableEntity.PartitionKey, tableEntity.RowKey);
            dynTableEntity.Properties.Add("Deleted", new EntityProperty(true));
            await _tableCli.MergeDynamicEntityAsync(dynTableEntity);
        }
    }
}
