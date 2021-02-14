using Application.DataTransfer;
using AzureTableDataAccess;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AzureTableDataAccess.Entities;
using Application.Commands;
using Application.Requests;
using Microsoft.Azure.Documents.SystemFunctions;

namespace Implementation.Commands
{
    public class TableCliUpdateProjectAsync : IUpdateProjectCommandAsync
    {        
        private readonly TableCli _tableCli;
        private readonly IMapper _mapper;

        public TableCliUpdateProjectAsync(IMapper mapper)
        {
            _tableCli = new TableCli(AzureStorageConnection.Instance(), "Projects");;
            _mapper = mapper;
        }

        public int Id => 20;

        public string Name => "Updating project record using Table Storage API";

        public async Task Execute(ProjectDto request)
        {
            // mapping to DataAccess object
            //if (!request.Id.Contains('$')) throw new Exception("not exist");
            var projectEntity = _mapper.Map<Project>(request);
            var contextRecord = await _tableCli.GetSingleEntity<Project>(projectEntity.PartitionKey, projectEntity.RowKey);
            //if (contextRecord.Deleted) throw new Exception("not exist");
            await _tableCli.MergeEntityAsync(projectEntity);
            
        }
    }
}
