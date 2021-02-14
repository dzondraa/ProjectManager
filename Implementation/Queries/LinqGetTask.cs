using Application;
using Application.DataTransfer;
using Application.Queries;
using AutoMapper;
using AzureTableDataAccess;
using AzureTableDataAccess.Entities;
using Implementation.Core;
using Microsoft.Azure.Cosmos.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Queries
{
    public class LinqGetTask : IGetTask
    {
        private readonly TableCli _tableCli;
        private readonly IMapper _mapper;
        public int Id => 50;

        public string Name => "Get single task using Table Client";

        public LinqGetTask(IMapper mapper)
        {
            _mapper = mapper;
            _tableCli = new TableCli(AzureStorageConnection.Instance(), "Tasks");
        }

        public Tasks Execute(TaskDto search)
        {
            var tableEntity = _mapper.Map<Tasks>(search);
            var task = _tableCli.GetSingleEntity<Tasks>(tableEntity.PartitionKey, tableEntity.RowKey);
            task.Wait();
            return task.Result as Tasks;
        }

    }
}
