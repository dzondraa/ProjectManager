using Api.Searches;
using Application;
using Application.DataTransfer;
using Application.Queries;
using Application.Searches;
using AutoMapper;
using AzureTableDataAccess;
using AzureTableDataAccess.Entities;
using Implementation.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos.Table;
using Microsoft.Azure.Documents.SystemFunctions;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Queries
{
    public class TableCliQueryTask : IQueryTask
    {
        private readonly TableCli _tableCli;
        private readonly IMapper _mapper;

        public int Id => 3;

        public string Name => "Querying tasks data using Table client";

        public TableCliQueryTask(IMapper mapper)
        {
            _mapper = mapper;
            _tableCli = new TableCli(AzureStorageConnection.Instance(), "Tasks");
        }

        public PagedResponse<TaskDto> Execute(TaskSearch search)
        {
            IQueryable<AzureTableDataAccess.Entities.Tasks> query;
            query = _tableCli.table.CreateQuery<AzureTableDataAccess.Entities.Tasks>();

            if (!String.IsNullOrWhiteSpace(search.Name))
                query = query.Where(x => x.Name == search.Name);

            if (!String.IsNullOrWhiteSpace(search.Description))
                query = query.Where(x => x.Name == search.Name);
            if (!String.IsNullOrWhiteSpace(search.ProjectId))
                query = query.Where(x => x.PartitionKey == search.ProjectId);

            query = query.Where(x => !x.Deleted);
            var result = query.ToList();
            List<TaskDto> mapped = _mapper.Map<List<Tasks>, List<TaskDto>>(result);

            // Only if consumers wants additional data, bacause it consuming more time to get additional data
            var counter = -1;
            Parallel.ForEach(result, (singleTask) =>
            {
                // Get dynamic properties for each entity
                counter++;
                var task = _tableCli.GetSingleDynamicEntity(singleTask.PartitionKey, singleTask.RowKey);
                task.Wait();
                var dto = Helper.toTaskDto(task.Result);
                mapped[counter].AdditionalFields = dto.AdditionalFields;
            });
            
                   
            
            
          
            return mapped.ToPagedResponse();
        }
    }
}
