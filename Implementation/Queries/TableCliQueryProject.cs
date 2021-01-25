﻿using Api.Searches;
using Application;
using Application.DataTransfer;
using Application.Queries;
using AzureTableDataAccess;
using AzureTableDataAccess.Entities;
using Implementation.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Documents.SystemFunctions;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Queries
{
    public class TableCliQueryProject : IQueryProject
    {
        private readonly TableCli _tableCli;
        public int Id => 3;

        public string Name => "Querying data using Table client";

        public TableCliQueryProject()
        {
            _tableCli = new TableCli(AzureStorageConnection.Instance(), "Projects");
        }

        public PagedResponse<Project> Execute(ProjectSearch search)
        {
            var query = _tableCli.table.CreateQuery<Project>();

            if (!String.IsNullOrWhiteSpace(search.Name))
                query.Where(x => x.Name == search.Name);

            return query.ToPagedResponse();
        }

    }
}
