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

        public PagedResponse<Project> Execute(ProjectDto search)
        {
            var tableEntity = _mapper.Map<Project>(search);
            var query = _tableCli.table
                .CreateQuery<Project>()
                .Where(x => x.PartitionKey == tableEntity.PartitionKey && x.RowKey == tableEntity.RowKey);
            return query.ToPagedResponse();
        }
    }
}
