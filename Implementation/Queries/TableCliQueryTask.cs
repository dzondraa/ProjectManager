using Api.Searches;
using Application;
using Application.DataTransfer;
using Application.Queries;
using Application.Searches;
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
        public int Id => 3;

        public string Name => "Querying tasks data using Table client";

        public TableCliQueryTask()
        {
            _tableCli = new TableCli(AzureStorageConnection.Instance(), "Tasks");
        }

        public PagedResponse<AzureTableDataAccess.Entities.Tasks> Execute(TaskSearch search)
        {
            IQueryable<AzureTableDataAccess.Entities.Tasks> query;
            query = _tableCli.table.CreateQuery<AzureTableDataAccess.Entities.Tasks>();

            if (!String.IsNullOrWhiteSpace(search.Name))
                query = query.Where(x => x.Name == search.Name);

            if (!String.IsNullOrWhiteSpace(search.Description))
                query = query.Where(x => x.Name == search.Name);

            return query.ToPagedResponse();
        }
    }
}
