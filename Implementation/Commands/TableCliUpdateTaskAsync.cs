using Application.Commands;
using Application.DataTransfer;
using Application.Requests;
using AutoMapper;
using AzureTableDataAccess;
using AzureTableDataAccess.Entities;
using Implementation.Validatiors;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Commands
{
    public class TableCliUpdateTaskAsync : IUpdateTaskAsync
    {
        private readonly TableCli _tableCli;
        private readonly IMapper _mapper;

        public TableCliUpdateTaskAsync(IMapper mapper)
        {
            _tableCli = new TableCli(AzureStorageConnection.Instance(), "Tasks"); ;
            _mapper = mapper;
        }

        public int Id => 26;

        public string Name => "Updating project record using Table Storage API";

        public async Task Execute(TaskDto request)
        {
            // mapping to DataAccess object
            var projectEntity = _mapper.Map<Tasks>(request);
            await _tableCli.MergeEntityAsync(projectEntity);
        }
    }
}
