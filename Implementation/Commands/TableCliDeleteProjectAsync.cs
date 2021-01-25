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
    public class TableCliDeleteProjectAsync : IDeleteProjectAsync
    {
        private readonly TableCli _tableCli;
        private readonly IMapper _mapper;
        public int Id => 2;

        public string Name => "Async deleting project using TableCli";

        public TableCliDeleteProjectAsync(IMapper mapper)
        {
            _mapper = mapper;
            _tableCli = new TableCli(AzureStorageConnection.Instance(), "Projects");
        }

        public async Task Execute(ProjectDto request)
        {
            //await _tableCli.MergeEntityAsync(new Project("partition1", request.Id, request.Name));

            // Mapping to dto to entity object - compatibile for storage operations
            var tableEntity = _mapper.Map<Project>(request);
            var dynTableEntity = new DynamicTableEntity(tableEntity.PartitionKey, tableEntity.RowKey);
            dynTableEntity.Properties.Add("Deleted", new EntityProperty(true));
            await _tableCli.MergeEntityAsync(dynTableEntity);
        }
    }
}
