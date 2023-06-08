using Api.Searches;
using Application.DataTransfer;
using Application.Queries;
using AutoMapper;
using AzureTableDataAccess;
using AzureTableDataAccess.Entities;
using Implementation.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Implementation.Queries
{
    public class TableCliQueryProject : IQueryProject
    {
        private readonly TableCli _tableCli;
        private readonly IMapper _mapper;

        public int Id => 3;

        public string Name => "Querying data using Table client";

        public TableCliQueryProject(IMapper mapper)
        {
            _tableCli = new TableCli(AzureStorageConnection.Instance(), "Projects");
            _mapper = mapper;
        }

        public PagedResponse<ProjectDto> Execute(ProjectSearch search)
        {
            IQueryable<Project> query;
            query = _tableCli.table.CreateQuery<Project>();

            if (!String.IsNullOrWhiteSpace(search.Name))
                query = query.Where(x => x.Name == search.Name);
            query = query.Where(x => !x.Deleted);
            var result = query.ToList();

            List<ProjectDto> mapped = _mapper.Map<List<Project>, List<ProjectDto>>(result);

            return mapped.ToPagedResponse();
        }

    }
}
