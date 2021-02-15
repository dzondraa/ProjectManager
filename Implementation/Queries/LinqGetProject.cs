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
    public class LinqGetProject : IGetProject
    {

        private readonly TableCli _tableCli;
        private readonly IMapper _mapper;
        public int Id => 10;

        public string Name => "Get single project using Table Client";

        public LinqGetProject(IMapper mapper)
        {
            _mapper = mapper;
            _tableCli = new TableCli(AzureStorageConnection.Instance(), "Projects");
        }

        public ProjectDto Execute(ProjectDto search)
        {
            var tableEntity = _mapper.Map<Project>(search);
            var task = _tableCli.GetSingleEntity<Project>(tableEntity.PartitionKey, tableEntity.RowKey);
            task.Wait();
            var result = task.Result as Project;
            return _mapper.Map<ProjectDto>(result);
        }
    }
}
